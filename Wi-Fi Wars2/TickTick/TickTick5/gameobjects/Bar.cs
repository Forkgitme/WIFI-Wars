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
    AnimatedGameObject car;
    int type;
    bool active, playsound, done;

    public Bar(int t, string id, int layer = 9, int sheetIndex = 1)
        : base("Sprites/bar2", layer, id, sheetIndex)
    {
        playsound = true;
        type = t;
        this.Position = new Vector2(20, 30*t);
        if (type == 4)
            this.position += new Vector2(0, 20);
        barPart = WifiWars.AssetManager.Content.Load<Texture2D>("Sprites/bar");
        car = new AnimatedGameObject();
        car.LoadAnimation("Sprites/Police@2", "car", true);
        resource = 0;
        totalResource = 1000;
        car.PlayAnimation("car");
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        if (!done)
        {
            GameObjectList level = this.parent as GameObjectList;
            level.Add(car);
            done = true;
        }

    }

    public override void Draw(GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
    {
        Color barColor = Color.White;
        if (type == 1)
        {
            barColor = Color.Red;
            this.Sprite.SpriteColor = Color.Red;
        }
        else if (type == 2)
        {
            barColor = Color.Purple;
            this.Sprite.SpriteColor = Color.Purple;
        }
        else if (type == 3)
        {
            barColor = Color.Yellow;
            this.Sprite.SpriteColor = Color.Yellow;
        }
        else if (type == 4)
        { 
            barColor = Color.Blue;
            this.Sprite.SpriteColor = Color.Blue;
            car.Position = new Vector2((float)(this.Position.X + this.sprite.Width * ((double)resource / totalResource)), this.Position.Y);
        }
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
            resource += 0.2f;
        }
        else if (resource >= totalResource && type == 4 && active)
        {
            if (playsound)
            {
                GameEnvironment.AssetManager.PlaySound("Sounds/Police");
                playsound = false;
            }
            GameEnvironment.GameStateManager.SwitchTo("gameOverState");
        }

   
      
 
    }

    public float Resource
    {
        get { return resource; }
        set { if (value <= totalResource) resource = value; }
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

