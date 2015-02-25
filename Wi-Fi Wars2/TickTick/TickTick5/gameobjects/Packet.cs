using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    class Packet : SpriteGameObject
    {
        public Packet(Vector2 spawnposition, Color c) : base("Sprites/Packet", 100, "packet", 0)
        {
            sprite.SpriteColor = c;
            velocity = new Vector2(50, 50);
            position = spawnposition;
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
