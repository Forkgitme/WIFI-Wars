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
        }
        public override void HandleInput(InputHelper inputHelper)
        {
            if (!placed)
                this.position = inputHelper.MousePosition - this.Center;            
            if (inputHelper.MouseLeftButtonPressed())
            {
                Level level = this.parent as Level;
                SpriteGameObject background = level.Find("background") as SpriteGameObject;
                if (background.Sprite.GetPixelColor((int)position.X + (int)Center.X, (int)position.Y + (int)Center.Y).G == 255)
                {
                    this.placed = true;
                    level.HoldingTower = false;
                }
            }
        }
    }
