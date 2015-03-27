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
        float[] Lengths;

        int StageIndex;
        float StagePos;
        float Speed = 100;

        static int bufferAmount;
        protected static bool holdingPacket;
        int type;
        protected bool inBuffer, held;
        public bool remove;
        protected Server serv;
        
        public Packet(Vector2 spawnposition, Color c, int t, Server server, GameObjectList TowerList, SpriteGameObject Home, string assetname = "Sprites/Packet")
            : base(assetname, 100, "packet", 0)
        {
            sprite.SpriteColor = c;
            position = spawnposition;
            type = t;

            serv = server;
 
            Path = FindPad(server as SpriteGameObject, TowerList, Home);

          
            ArrayofArrays = ArrayWithDirectionsAndLengthofPath(Path);

            Directions= ArrayofArrays[0] as Vector2[];

            Lengths = ArrayofArrays[1] as float[];

        }

        public override void HandleInput(InputHelper inputHelper)
        {
            
            base.HandleInput(inputHelper);
            Level level = this.Parent.Parent as Level;
            Home home = level.Find("home") as Home;
            if (inBuffer)
            {
                if (inputHelper.MousePosition.X < this.Position.X + 15 && inputHelper.MousePosition.X > this.Position.X && inputHelper.MousePosition.Y < this.Position.Y + 15 && inputHelper.MousePosition.Y > this.Position.Y)
                    if (inputHelper.MouseLeftButtonPressed())
                    {
                        if (!holdingPacket)
                        {
                            this.Velocity = Vector2.Zero;
                            holdingPacket = true;
                            held = true;
                        }
                        else
                        {
                            remove = true;
                            holdingPacket = false;
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
                    remove = true;
                }
                if (serverBar2 != null)
                    if (this.CollidesWith(serverBar2))
                    {
                        level.Remove(this);
                        bufferAmount -= 1;
                        holdingPacket = false;
                        remove = true;
                    }
                if (serverBar3 != null)
                    if (this.CollidesWith(serverBar3))
                    {
                        level.Remove(this);
                        bufferAmount -= 1;
                        holdingPacket = false;
                        remove = true;
                    }           
                if (held)
                    this.Position = inputHelper.MousePosition - Center;
            }
                                
            if (this.CollidesWith(home))
            {
                Bar bar = GameWorld.Find("police") as Bar;
                bar.Active = true;
                if (!home.active)
                {                    
                    if (bufferAmount < 5)
                    {
                        this.Position = new Vector2(342, 107);
                        this.Velocity = new Vector2(WifiWars.Random.Next(-50, 51), WifiWars.Random.Next(-50, 51));
                        inBuffer = true;
                        bufferAmount += 1;
                    }
                    else
                    {
                        remove = true;
                    }
                }
                else
                {
                    Bar serverBar = GameWorld.Find("serverbar" + type) as Bar;
                    serverBar.Resource += 50;
                    remove = true;
                }
            }
            if (remove)
            {
                GameObjectList parent = this.parent as GameObjectList;
                parent.Remove(this);
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
                if(tower.connected)
                OpenList.Add(tower);

            ClosedList.Add(server);

            int n = 0;

            //Make shortest path add it to the closedlist
            while (!ClosedList.Contains(home))
            {
                SpriteGameObject Node = ClosedList.ElementAt(n) as SpriteGameObject;

                foreach (SpriteGameObject obj in OpenList)
                {
                    if (obj.CollidesWith(Node) && !(ClosedList.Contains(obj)) && obj.CollidesWith(obj))
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

            Path.Add(new Vector2(home.Position.X,home.Position.Y+home.Height/2));

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

            

            

         
            if (inBuffer)
            {      
            SpriteGameObject buffer = GameWorld.Find("buffer") as SpriteGameObject;
            if (!buffer.Visible)
            {
                this.remove = true;
                bufferAmount = 0;
            }
            else if (this.CollidesWith(buffer))
                this.Velocity = -this.Velocity;               
            }     
            else if(StageIndex != Path.Count-1)
            {
                velocity = Directions[StageIndex]*Speed;
                StagePos += (float)gameTime.ElapsedGameTime.TotalSeconds * Speed;

                if(StagePos > Lengths[StageIndex])
                {
                    StageIndex++;
                    StagePos = 0;
                }                           
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) //draw het packet op de nieuwe locatie
        {
            base.Draw(gameTime, spriteBatch);
        }

    }
