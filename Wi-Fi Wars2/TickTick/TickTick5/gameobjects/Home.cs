using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Home : SpriteGameObject
{
    public bool active;
    public Home(Vector2 pos, int layer, String id)
        : base("Sprites/home", layer, id)
    {
        position = pos;
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (inputHelper.MousePosition.X < this.Position.X + 58 && inputHelper.MousePosition.X > this.Position.X + 22 && inputHelper.MousePosition.Y < this.Position.Y + 15 && inputHelper.MousePosition.Y > this.Position.Y)
            if (inputHelper.MouseLeftButtonPressed())
            {
                GameObjectList level = this.parent as GameObjectList;
                UI ui = level.Find("ui") as UI;
                if (!active)
                {
                    this.Sprite = new SpriteSheet("Sprites/homeActive", 0);
                    active = true;
                    SpriteGameObject buffer = ui.Find("buffer") as SpriteGameObject;
                    buffer.Visible = false;
                    GameEnvironment.AssetManager.PlaySound("Sounds/snd_switch");
                }
                else
                {
                    this.Sprite = new SpriteSheet("Sprites/home", 0);
                    active = false;
                    SpriteGameObject buffer = ui.Find("buffer") as SpriteGameObject;
                    buffer.Visible = true;
                    GameEnvironment.AssetManager.PlaySound("Sounds/snd_switch");
                }
            }
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (active)
        {
            GameObjectList level = this.Parent as GameObjectList;
            UI ui = level.Find("ui") as UI;
            if (ui.Money >= 0.02)
            ui.Money -= 0.02f;
            else
            {
                this.Sprite = new SpriteSheet("Sprites/home", 0);
                active = false;
                SpriteGameObject buffer = ui.Find("buffer") as SpriteGameObject;
                buffer.Visible = true;
                GameEnvironment.AssetManager.PlaySound("Sounds/snd_switch");
            }
        }
    }
}
