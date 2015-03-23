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
        if (inputHelper.MousePosition.X < this.Position.X + 20 && inputHelper.MousePosition.X > this.Position.X && inputHelper.MousePosition.Y < this.Position.Y + 20 && inputHelper.MousePosition.Y > this.Position.Y)
            if (inputHelper.MouseLeftButtonPressed())
            {
                GameObjectList level = this.parent as GameObjectList;
                UI ui = level.Find("ui") as UI;
                if (!active && ui.Money >= 100)
                {
                    this.Sprite = new SpriteSheet("Sprites/homeActive", 0);
                    active = true;
                    ui.Money -= 100;
                    ui.Remove(ui.Find("buffer"));
                }
            }
    }
}
