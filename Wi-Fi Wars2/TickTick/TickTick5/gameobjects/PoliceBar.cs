using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


class PoliceBar : SpriteGameObject
{
    float resource;         
    int totalResource;      
    Texture2D barPart;   
  
    public PoliceBar(string id = "policebar", int layer = 9, int sheetIndex = 1)
        : base("Sprites/policebar2", layer, id, sheetIndex)
    {
        this.Position = new Vector2(300, 60);
        barPart = WifiWars.AssetManager.Content.Load<Texture2D>("Sprites/policebar");
        resource = 0;
        totalResource = 1000;
    }

    public override void Draw(GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
    {
            base.Draw(gameTime, spriteBatch);
            spriteBatch.Draw(barPart,
                             new Rectangle((int)Position.X,
                                          (int)Position.Y,
                                          (int)(sprite.Width * ((double)resource / totalResource)),
                                          sprite.Height),
                             Color.Blue);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (resource<totalResource)
        resource += 0.2f;
    }

    public float Resource
    {
        get { return resource; }
        set { resource = value; }
    }

    public int TotalResource
    {
        get { return totalResource; }
        set { totalResource = value; }
    }
}

