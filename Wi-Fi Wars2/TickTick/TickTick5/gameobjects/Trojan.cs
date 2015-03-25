using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

class Trojan : Virus
{
    bool hidden = true;

    public Trojan(Vector2 spawnPosition, Server server, GameObjectList list, SpriteGameObject obj)
        : base(spawnPosition, server, list ,obj)
    {
        this.Sprite = new SpriteSheet("Sprites/TrojanHorse", 0);
    }

    public override void Changes()
    {
        if (hidden)
        {
            hidden = false;
            this.Sprite = new SpriteSheet("Sprites/Virus", 0);
        }
        else
        {
            delete = true;
        }
    }
}

