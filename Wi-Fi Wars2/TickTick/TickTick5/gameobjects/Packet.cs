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
        public override void Update(GameTime gameTime) //de locatie van het packet blijven updaten
        {
            base.Update(gameTime);    

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) //draw het packet op de nieuwe locatie
        {
            base.Draw(gameTime, spriteBatch);
        }

    }
