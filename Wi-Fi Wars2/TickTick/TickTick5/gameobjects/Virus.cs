using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

class Virus : Packet
{
    public Virus(Vector2 spawnPosition)
        : base(spawnPosition, Color.White, 4)
    {
    }
}

