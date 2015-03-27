using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

    class Firewall : AnimatedGameObject
    {
        bool placed, inCooldown = false;
        int timer = 10000;  // cooldown timer
        float rotation;

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
                Level level = this.Parent.Parent as Level;
                SpriteGameObject background = level.Find("background") as SpriteGameObject;
                if (background.Sprite.GetPixelColor((int)position.X + (int)Center.X, (int)position.Y + (int)Center.Y).G >= 250)
                {
                    this.Sprite.SpriteColor = Color.White;
                    if (inputHelper.MouseLeftButtonPressed())
                    {
                        GameEnvironment.AssetManager.PlaySound("Sounds/CD Drive");
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
                rotation = Sprite.Rotation;
                if (inputHelper.MouseRightButtonPressed())
                {
                    level.Holding = false;
                    UI ui = level.Find("ui") as UI;
                    ui.Money += 20;
                    GameObjectList firewallList = this.parent as GameObjectList;
                    firewallList.Remove(this);
                }
            }
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);

            if (inCooldown)
            {
                timer -= gameTime.ElapsedGameTime.Milliseconds;
                if (timer < 0)
                {
                    this.InCooldown = false;
                    timer = 10000;
                }
            }
        }

        public bool InCooldown
        {
            get { return inCooldown; }
            set {
                    if (value == true)
                    {
                        inCooldown = true;
                        this.PlayAnimation("idle");
                        Sprite.Rotation = rotation;
                    }
                    else
                    {
                        inCooldown = false;
                        this.PlayAnimation("burning");
                        Sprite.Rotation = rotation;
                    }
                }
        }

        public bool Placed
        {
            get { return placed; }
        }
    }
