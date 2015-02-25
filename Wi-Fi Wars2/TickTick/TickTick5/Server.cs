using System;
using System.IO;
using Microsoft.Xna.Framework;

class Server : SpriteGameObject
{
    Color serverColor;

    public Server(int color, Vector2 pos, int layer, String id)
        : base("server", layer, id)
    {
        position = pos;
        
        switch(color)
        {
            case 1:
                serverColor = Color.Red;
                break;
            case 2:
                serverColor = Color.Purple;
                break;
            case 3:
                serverColor = Color.Yellow;
                break;
            default: throw new IOException("Invalid colour code: " + color);
        }
    }
}