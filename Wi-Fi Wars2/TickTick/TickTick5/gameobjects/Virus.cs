using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

class Virus : Packet
{
    public Virus(Vector2 spawnPosition, Server server, GameObjectList list, SpriteGameObject obj)
        : base(spawnPosition, Color.White, 4, server, list ,obj)
    {
    }
}

