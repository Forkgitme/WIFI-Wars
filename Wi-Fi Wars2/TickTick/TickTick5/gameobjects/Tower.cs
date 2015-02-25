using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

    class Tower : SpriteGameObject
    {
        bool placed;
        bool connected;
        public Tower(int layer = 2, string id = "tower") : base("Sprites/tower", layer, id) 
        {
            this.Position = new Vector2(-1000, -1000);
        }
        public override void HandleInput(InputHelper inputHelper)
        {
            GameObjectList towerList = this.parent as GameObjectList;
            Level level = towerList.Parent as Level;
            SpriteGameObject background = level.Find("background") as SpriteGameObject;
            if (!placed && inputHelper.MousePosition.X > 0 && inputHelper.MousePosition.Y > 0 && inputHelper.MousePosition.Y < 825 && inputHelper.MousePosition.X < 1440)
            {
                this.position = inputHelper.MousePosition - this.Center;
                if (background.Sprite.GetPixelColor((int)position.X + (int)Center.X, (int)position.Y + (int)Center.Y).G == 255)
                {
                    this.Sprite.SpriteColor = Color.White;
                    if (inputHelper.MouseLeftButtonPressed())
                    {
                        this.placed = true;
                        level.HoldingTower = false;
                    }
                }
                else
                    this.Sprite.SpriteColor = Color.Red;
            }
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            SpriteGameObject home = GameWorld.Find("home") as SpriteGameObject;
            if (placed)
            { 
            if (this.CollidesWith(home))
                connected = true;
            GameObjectList towerList = GameWorld.Find("towerlist") as GameObjectList;
            List<GameObject> towers = towerList.Objects;
            foreach (Tower tower in towers)
                if (this.CollidesWith(tower) && tower.Connected)
                    this.connected = true;
            }
        }
        public bool Connected
        {
            get { return connected; }
            set { connected = value; }
        }
    }
