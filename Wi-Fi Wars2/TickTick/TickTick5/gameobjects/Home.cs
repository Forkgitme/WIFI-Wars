using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Home : SpriteGameObject
{
    public Home(Vector2 pos, int layer, String id)
        : base("Sprites/home", layer, id)
    {
        position = pos;
    }
}
