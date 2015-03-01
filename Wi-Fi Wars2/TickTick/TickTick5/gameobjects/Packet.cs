using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    class Packet : SpriteGameObject
    {



        static int bufferAmount;
        static bool holdingPacket;
        double velocityTimer;
        int type;
        bool inBuffer, held;
        public Packet(Vector2 spawnposition, Color c, int t) : base("Sprites/Packet", 100, "packet", 0)
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
        public void FindPadPlease(SpriteGameObject server)
        {
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

                    n++;
                }  


        }

  
        public override void Update(GameTime gameTime) //de locatie van het packet blijven updaten
        {
            base.Update(gameTime);
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
