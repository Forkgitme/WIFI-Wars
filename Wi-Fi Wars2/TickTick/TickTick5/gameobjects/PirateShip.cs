using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class PirateShip : SpriteGameObject
{
    bool inNetwork;
    double randomTimer;
    bool remove;

    public PirateShip(Vector2 pos, int layer, String id)
        : base("Sprites/Pirateship", layer, id)
    {
        position = pos;
        Vector2 randomVector = new Vector2(WifiWars.Random.Next(-100, 51), WifiWars.Random.Next(-100, 101));
        randomVector.Normalize();
        this.velocity = randomVector * 50;
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (remove)
        {
            GameObjectList parent = this.parent as GameObjectList;
            parent.Remove(this);
        }
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        inNetwork = false;
        randomTimer += gameTime.ElapsedGameTime.TotalSeconds;
        if (randomTimer > 10)
        {
            randomTimer = 0;
            this.Velocity = new Vector2(WifiWars.Random.Next(-50, 51), WifiWars.Random.Next(-50, 51));
        }   
        GameObjectList level = this.parent as GameObjectList;
        GameObjectList TowerList = level.Find("towerlist") as GameObjectList;
        List<GameObject> Towers = TowerList.Objects;
            foreach (Tower tower in Towers)
            {
                if (Math.Sqrt(Math.Pow((this.position.X - tower.Position.X - tower.Center.X), 2) + Math.Pow((this.position.Y - tower.Position.Y - tower.Center.Y), 2)) < 100)
                    inNetwork = true;

            }
            if (!inNetwork)
                velocity = -velocity;
            GameObjectList packetList = level.Find("packetList") as GameObjectList;
            List<GameObject> packets = packetList.Objects;
        foreach (Packet packet in packets)
        {
            if (Math.Sqrt(Math.Pow((this.position.X - packet.Position.X - packet.Center.X), 2) + Math.Pow((this.position.Y - packet.Position.Y - packet.Center.Y), 2)) < 100)
            {
                Vector2 direction = new Vector2(packet.Position.X - this.position.X, packet.Position.Y - this.position.Y);
                direction.Normalize();
                this.Velocity = direction * 50;
            }
            if (this.CollidesWith(packet))
            { 
                packet.remove = true;
                remove = true;
            }
        }
    }
}
