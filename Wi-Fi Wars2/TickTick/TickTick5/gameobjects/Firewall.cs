using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

    class Firewall : AnimatedGameObject
    {
        bool placed;
        public Firewall()
            : base(1, "firewall")
        {
            this.LoadAnimation("Sprites/firewall@5", "burning", true, 0.1f);
            this.LoadAnimation("Sprites/firewall", "idle", true, 0.2f);
            this.PlayAnimation("burning");
        }

      
        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (!placed && inputHelper.MousePosition.X > 0 && inputHelper.MousePosition.Y > 0 && inputHelper.MousePosition.Y < 825 && inputHelper.MousePosition.X < 1440)
            {
                this.position = inputHelper.MousePosition;
                Level level = this.Parent as Level;
                SpriteGameObject background = level.Find("background") as SpriteGameObject;
                if (background.Sprite.GetPixelColor((int)position.X + (int)Center.X, (int)position.Y + (int)Center.Y).G == 255)
                {
                    this.Sprite.SpriteColor = Color.White;
                    if (inputHelper.MouseLeftButtonPressed())
                    {
                        this.placed = true;
                        level.Holding = false;                    
                    }
                }
                else
                    this.Sprite.SpriteColor = Color.Red;
                if (inputHelper.IsKeyDown(Keys.D))
                    this.Sprite.Rotation += 0.01f;
                if (inputHelper.IsKeyDown(Keys.A))
                    this.Sprite.Rotation -= 0.01f;
            }
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
