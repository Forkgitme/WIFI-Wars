using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

    class Tower : SpriteGameObject
    {
        bool placed;
        public bool connected;
        bool based;
        SpriteGameObject towerBase;
        public Tower(int layer = 2, string id = "tower") : base("Sprites/Range", layer, id) 
        {
            this.Position = new Vector2(-1000, -1000);
        }
        public override void HandleInput(InputHelper inputHelper)
        {
            if (!based)
            {
                towerBase = new SpriteGameObject("Sprites/tower", 10);
                GameObjectList towerList = this.parent as GameObjectList;
                Level level = towerList.Parent as Level;
                level.Add(towerBase);
                based = true;
            }
            if (!placed && inputHelper.MousePosition.X > 0 && inputHelper.MousePosition.Y > 0 && inputHelper.MousePosition.Y < 825 && inputHelper.MousePosition.X < 1440)
            {
                this.position = inputHelper.MousePosition - this.Center;
                towerBase.Position = inputHelper.MousePosition - towerBase.Center;
                GameObjectList towerList = this.parent as GameObjectList;
                Level level = towerList.Parent as Level;
                SpriteGameObject background = level.Find("background") as SpriteGameObject;
                if (background.Sprite.GetPixelColor((int)position.X + (int)Center.X, (int)position.Y + (int)Center.Y).G == 255)
                {
                    this.Sprite.SpriteColor = Color.White;
                    if (inputHelper.MouseLeftButtonPressed())
                    {
                        this.placed = true;
                        level.Holding = false;
                        List<GameObject> towers = towerList.Objects;
                        foreach (Tower tower in towers)
                            CheckConnection();
                        Server server1 = GameWorld.Find("server1") as Server;
                        if (server1 != null)
                            server1.CheckConnection();
                        Server server2 = GameWorld.Find("server2") as Server;
                        if (server2 != null)
                            server2.CheckConnection();
                        Server server3 = GameWorld.Find("server3") as Server;
                        if (server3 != null)
                            server3.CheckConnection();
                        GameObjectList hideoutList = level.Find("hideoutlist") as GameObjectList;
                        List<GameObject> hideouts = hideoutList.Objects;
                        foreach (Hideout hideout in hideouts)
                            hideout.CheckConnection();
                        GameObjectList antihideoutList = level.Find("antihideoutlist") as GameObjectList;
                        List<GameObject> antihideouts = antihideoutList.Objects;
                        foreach (AntiPirateHideout antihideout in antihideouts)
                            antihideout.CheckConnection();
                    }

                }
                else 
                    this.Sprite.SpriteColor = Color.Red;
            }
        }
        public void CheckConnection()
        {
            if (placed && !connected)
            {
            SpriteGameObject home = GameWorld.Find("home") as SpriteGameObject;
            if (this.CollidesWith(home))
                connected = true;
            GameObjectList towerList = GameWorld.Find("towerlist") as GameObjectList;
            List<GameObject> towers = towerList.Objects;
            foreach (Tower tower in towers)
                if (this.CollidesWith(tower) && tower.Connected)
                    this.connected = true;
            if (connected)
            { 
                GameObjectList towerList2 = GameWorld.Find("towerlist") as GameObjectList;
                    List<GameObject> towers2 = towerList2.Objects;
                    foreach (Tower tower2 in towers2)
                            tower2.CheckConnection();
            }
            }
        }
        public bool Connected
        {
            get { return connected; }
            set { connected = value; }
        }
    }
