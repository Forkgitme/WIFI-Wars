using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    class Packet : SpriteGameObject
    {

        List<Vector2> Path;
        Array[] ArrayofArrays;
        Vector2[] Directions;

        int StageIndex;

        static int bufferAmount;
        static bool holdingPacket;
        int type;
        bool inBuffer, held;
        public Packet(Vector2 spawnposition, Color c, int t, Server server, GameObjectList TowerList, SpriteGameObject Home)
            : base("Sprites/Packet", 100, "packet", 0)
        {
            sprite.SpriteColor = c;
            position = spawnposition;
            type = t;
            
            
            Path = FindPad(server as SpriteGameObject, TowerList, Home);

          
            ArrayofArrays = ArrayWithDirectionsAndLengthofPath(Path);

            Directions= ArrayofArrays[0] as Vector2[];

            float[] Lengths = ArrayofArrays[1] as float[];

        }

        public override void HandleInput(InputHelper inputHelper)
        {
            
            base.HandleInput(inputHelper);

            SpriteGameObject home = GameWorld.Find("home") as SpriteGameObject;

            if (inBuffer)
            {            
                if (!holdingPacket)
                { 
                if (inputHelper.MousePosition.X < this.Position.X + Center.X + 10 && inputHelper.MousePosition.X > this.Position.X + Center.X - 10 && inputHelper.MousePosition.Y < this.Position.Y + Center.Y + 10 && inputHelper.MousePosition.Y > this.Position.Y + Center.Y - 10)
                    if (inputHelper.MouseLeftButtonPressed())
                    { 
                        this.Velocity = Vector2.Zero;
                        holdingPacket = true;
                        held = true;
                    }     
                }
                Bar serverBar = GameWorld.Find("serverbar" + type) as Bar;
                Bar serverBar2 = null;
                Bar serverBar3 = null;
                if (type == 1)
                {
                    serverBar2 = GameWorld.Find("serverbar" + 2) as Bar;
                    serverBar3 = GameWorld.Find("serverbar" + 3) as Bar;
                }
                else if (type == 2)
                {
                    serverBar2 = GameWorld.Find("serverbar" + 1) as Bar;
                    serverBar3 = GameWorld.Find("serverbar" + 3) as Bar;
                }
                else if (type == 3)
                {
                    serverBar2 = GameWorld.Find("serverbar" + 1) as Bar;
                    serverBar3 = GameWorld.Find("serverbar" + 2) as Bar;
                }
                if (this.CollidesWith(serverBar))
                { 
                    serverBar.Resource += 50;
                    bufferAmount -= 1;
                    holdingPacket = false;
                    Level level = this.Parent as Level;
                    level.Remove(this);
                }
                if (serverBar2 != null)
                    if (this.CollidesWith(serverBar2))
                    {
                        Level level = this.Parent as Level;
                        level.Remove(this);
                        bufferAmount -= 1;
                        holdingPacket = false;
                    }
                if (serverBar3 != null)
                    if (this.CollidesWith(serverBar3))
                    {
                        Level level = this.Parent as Level;
                        level.Remove(this);
                        bufferAmount -= 1;
                        holdingPacket = false;
                    }           
                if (held)
                    this.Position = inputHelper.MousePosition - Center;
            }
                                
            if (this.CollidesWith(home))
            {
                Level level = this.Parent as Level;
                Bar bar = GameWorld.Find("police") as Bar;
                bar.Active = true;
                if (bufferAmount <5)
                { 
                this.Position = new Vector2(670, 100);
                this.Velocity = new Vector2(WifiWars.Random.Next(-50, 51), WifiWars.Random.Next(-50, 51));
                inBuffer = true;
                bufferAmount += 1;
                }
                else
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
        public List<Vector2> FindPad(SpriteGameObject server, GameObjectList TowerList, SpriteGameObject home)
        {
            List<Vector2> Path = new List<Vector2>();
            List<float> TowersFvalues = new List<float>();
            List<GameObject> OpenList = new List<GameObject>();
            List<GameObject> ClosedList = new List<GameObject>();


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
                        float Fvalue = CalculateFvalue(obj.Position, Node.Position, home.Position);
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

                Path.Add(Node.Position + Node.Center);

                n++;
            }

            Path.Add(home.Position);

            return Path;

        }

        public Array[] ArrayWithDirectionsAndLengthofPath(List<Vector2> Path)
        {
            Array[] DirectionsAndLength = new Array[2];
            float[] Lengths = new float[Path.Count - 1];
            Vector2[] Directions = new Vector2[Path.Count - 1];
            for (int i = 0; i < Path.Count - 1; i++)
            {
                Directions[i] = Path[i + 1] - Path[i];
                Lengths[i] = Directions[i].Length();
                Directions[i].Normalize();
            }

            DirectionsAndLength[0] = Directions;
            DirectionsAndLength[1] = Lengths;

            return DirectionsAndLength;
        }
  
        public override void Update(GameTime gameTime) //de locatie van het packet blijven updaten
        {
            base.Update(gameTime);

            if(StageIndex != Path.Count-1)
            {
                velocity = Directions[StageIndex]*200;

             

            }

            

         
            if (inBuffer)
            {      
            SpriteGameObject buffer = GameWorld.Find("buffer") as SpriteGameObject;
            if (this.CollidesWith(buffer))
                this.Velocity = -this.Velocity;               
            }     
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) //draw het packet op de nieuwe locatie
        {
            base.Draw(gameTime, spriteBatch);
        }

    }
