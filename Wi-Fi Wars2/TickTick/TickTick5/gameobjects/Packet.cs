using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    class Packet : SpriteGameObject
    {
  
        int type;

        public Packet(Vector2 spawnposition, Color c, int t, string spritename = "Sprites/Packet")
            : base(spritename, 100, "packet", 0)
        {
            sprite.SpriteColor = c;
            velocity = new Vector2(50, 50);
            position = spawnposition;
            type = t;
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            
            base.HandleInput(inputHelper);

            SpriteGameObject home = GameWorld.Find("home") as SpriteGameObject;

            if (this.CollidesWith(home))
            {
                Bar serverBar = GameWorld.Find("serverbar" + type) as Bar;
                serverBar.Resource += 50;
                Level level = this.Parent as Level;
                Bar bar = GameWorld.Find("police") as Bar;
                bar.Active = true;
                level.Remove(this);
            }
        }

       //Used to determine the next node on the graph 
        public float CalculateFvalue(Vector2 a, Vector2 b, Vector2 c)
        {
            float Fvalue;

            Fvalue = Vector2.Distance(a, b) + Vector2.Distance(a, c);

            return Fvalue;
        }


        //Used to find a shortest path from server to home 
        public List<Vector2> FindPad(SpriteGameObject server)
        {
            List<Vector2> Path = new List <Vector2>();
            List<float> TowersFvalues = new List<float>();
            List<GameObject> OpenList = new List<GameObject>();
            List<GameObject> ClosedList = new List<GameObject>();

            GameObjectList TowerList = GameWorld.Find("towerlist") as GameObjectList;
            SpriteGameObject home = GameWorld.Find("home") as SpriteGameObject;

            OpenList.Add(home);
            List<GameObject> Towers = TowerList.Objects;
            foreach (Tower tower in Towers)
                OpenList.Add(tower);

            ClosedList.Add(server);

            int n = 0;

            //Make shortest path add it to the closedlist
            while (!ClosedList.Contains(home))
                {
                    SpriteGameObject Node = ClosedList.ElementAt(n) as SpriteGameObject;

                    foreach (SpriteGameObject obj in OpenList)
                    {
                        if (obj.CollidesWith(Node) && !(ClosedList.Contains(obj)))
                        {
                            float Fvalue = CalculateFvalue(obj.Position, Node.Position , home.Position);
                            TowersFvalues.Add(Fvalue);
                        }
                    }

                    float lowest = TowersFvalues.Min();

                    foreach (SpriteGameObject obj in OpenList)
                    {
                        if (obj.CollidesWith(Node) && CalculateFvalue(obj.Position, Node.Position, home.Position) == lowest)
                        {
                            ClosedList.Add(obj);
                            OpenList.Remove(obj);
                            TowersFvalues.Clear();
                            break;
                        }
                    }

                    Path.Add(Node.Position);

                    n++;
                }

            Path.Add(home.Position);

            return Path;

        }

        public Vector2 [] DirectionsOfPad(List <Vector2> Path)
        {
            Vector2 [] Directions = new Vector2[Path.Count - 1];

            for (int i = 0; i < Path.Count - 1; i++)
            {
                Directions[i] = Path[i + 1] - Path[i];
                Directions[i].Normalize();
            }

            return Directions;
        }

        public float [] LengthsOfPad(List <Vector2> Path)
        {
            float [] Lengths = new float[Path.Count - 1];
            Vector2 [] Directions = new Vector2[Path.Count - 1];
            for (int i = 0; i < Path.Count - 1; i++)
            {
                Directions[i] = Path[i + 1] - Path[i];
                Lengths[i] = Directions[i].Length();
                Directions[i].Normalize();
            }

            return Lengths;
        }
  
        public override void Update(GameTime gameTime) //de locatie van het packet blijven updaten
        {
            List<Vector2> Path = FindPad(GameWorld.Find("server1") as SpriteGameObject);
            Vector2 [] Directions = DirectionsOfPad(Path);
            float[] Lengths = LengthsOfPad(Path);

            base.Update(gameTime);    
            

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) //draw het packet op de nieuwe locatie
        {
            base.Draw(gameTime, spriteBatch);
        }

    }
