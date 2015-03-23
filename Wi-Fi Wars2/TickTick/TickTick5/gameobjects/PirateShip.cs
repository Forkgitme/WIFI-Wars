using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class PirateShip : AnimatedGameObject
{
    bool inNetwork;
    double randomTimer;
    public bool remove;
    bool breaking;
    public PirateShip(Vector2 pos, int layer, String id)
        : base(layer, "pirateship")
    {
        position = pos;
        Vector2 randomVector = new Vector2(WifiWars.Random.Next(-100, 51), WifiWars.Random.Next(-100, 101));
        randomVector.Normalize();
        this.velocity = randomVector * 50;
        this.LoadAnimation("Sprites/PirateBreak@10", "breaking", false, 0.1f);
        this.LoadAnimation("Sprites/Pirateship", "idle", true, 0.2f);
        this.PlayAnimation("idle");
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        if (!breaking)
        { 
        base.HandleInput(inputHelper);
        if (inputHelper.MousePosition.X < this.Position.X + 13 && inputHelper.MousePosition.X > this.Position.X + Center.X - 13 && inputHelper.MousePosition.Y < this.Position.Y && inputHelper.MousePosition.Y > this.Position.Y - 22)
                    if (inputHelper.MouseLeftButtonPressed())
                    {
                        this.PlayAnimation("breaking");
                        this.velocity = Vector2.Zero;
                        breaking = true;
                    }     
        }
        if (remove)
        {
            GameObjectList parent = this.parent as GameObjectList;
            parent.Remove(this);
        }
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (!breaking)
        {         
            inNetwork = false;
            randomTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (randomTimer > 10)
            {
                randomTimer = 0;
                this.Velocity = new Vector2(WifiWars.Random.Next(-50, 51), WifiWars.Random.Next(-50, 51));
            }
            GameObjectList level = this.parent.Parent as GameObjectList;
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
        else
        {
            this.PlayAnimation("breaking");
            if (this.Current.AnimationEnded)
            {
                remove = true;
            }
        }
    }
}
