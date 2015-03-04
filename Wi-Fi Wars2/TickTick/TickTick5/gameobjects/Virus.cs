using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

class Virus : Packet
{
    bool delete = false;
    public Virus(Vector2 spawnPosition, Server server, GameObjectList list, SpriteGameObject obj)
        : base(spawnPosition, Color.White, 4, server, list ,obj, "Sprites/tower")
    {
        
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        if (delete)
        {
            Level level = this.Parent as Level;
            level.Remove(this);
        }

    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        GameObjectList firewallList = GameWorld.Find("firewallList") as GameObjectList;
        List<GameObject> firewalls = firewallList.Objects;
        foreach (Firewall firewall in firewalls)
        {
            if (this.CollidesWith(firewall) && firewall.InCooldown == false && firewall.Placed)
            {
                firewall.InCooldown = true;
                delete = true;
            }
                
        }
    }
}

