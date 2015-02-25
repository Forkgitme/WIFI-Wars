using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

    class Tower : SpriteGameObject
    {
        bool placed;
        public Tower(int layer = 0, string id = "tower") : base("Sprites/tower", layer, id) 
        {
            this.Position = new Vector2(-1000, -1000);
        }
        public override void HandleInput(InputHelper inputHelper)
        {
            Level level = this.parent as Level;
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
    }
