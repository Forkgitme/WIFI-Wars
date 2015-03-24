using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


class Bar : SpriteGameObject
{
    float resource;         
    int totalResource;      
    Texture2D barPart;
    int type;
    bool active;

    public Bar(int t, string id, int layer = 9, int sheetIndex = 1)
        : base("Sprites/bar2", layer, id, sheetIndex)
    {
        type = t;
        this.Position = new Vector2(300, 30 + 30*t);
        barPart = WifiWars.AssetManager.Content.Load<Texture2D>("Sprites/bar");
        resource = 0;
        totalResource = 1000;
    }

    public override void Draw(GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
    {
        Color barColor = Color.White;
        if (type == 1)
            barColor = Color.Red;
        else if (type == 2)
            barColor = Color.Purple;
        else if (type == 3)
            barColor = Color.Yellow;
        else if (type == 4)
            barColor = Color.Blue;       
            base.Draw(gameTime, spriteBatch);
            spriteBatch.Draw(barPart,
                             new Rectangle((int)Position.X,
                                          (int)Position.Y,
                                          (int)(sprite.Width * ((double)resource / totalResource)),
                                          sprite.Height), barColor);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (resource < totalResource && type == 4 && active)
        {
            resource += 2.2f;
        }
        else if (resource >= totalResource && type == 4 && active)
        {
            GameEnvironment.GameStateManager.SwitchTo("gameOverState");
        }
    }

    public float Resource
    {
        get { return resource; }
        set { resource = value; }
    }

    public bool Active
    {
        get { return active; }
        set { active = value; }
    }

    public int TotalResource
    {
        get { return totalResource; }
        set { totalResource = value; }
    }
}

