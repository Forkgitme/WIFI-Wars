using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

class Virus : Packet
{
    protected bool delete = false;
    public Virus(Vector2 spawnPosition, Server server, GameObjectList list, SpriteGameObject obj)
        : base(spawnPosition, Color.White, 4, server, list, obj, "Sprites/Virus")
    {

    }

    public override void HandleInput(InputHelper inputHelper)
    {
        if((inputHelper.MousePosition.X > this.Position.X && inputHelper.MousePosition.X < this.Position.X + 15)&&(inputHelper.MousePosition.Y > this.Position.Y && inputHelper.MousePosition.Y < this.Position.Y + 15))
            if (!inputHelper.MouseLeftButtonDown())
            {
                this.Fades();
            }
        
        Level level = this.Parent as Level;
        Home home = level.Find("home") as Home;
        if (this.CollidesWith(home))
        {
            WifiWars.AssetManager.PlaySound("Sounds/badbleep");
            delete = true;
            for (int i = 1; i < 4; i++)
            {
                Bar serverBar = GameWorld.Find("serverbar" + i) as Bar;
                if (serverBar != null)
                {
                    if (serverBar.Resource >= 50)
                        serverBar.Resource -= 50;
                }
            }
        }
        if (delete)
        {
            Level levelu = this.Parent as Level;
            levelu.Remove(this);
        }
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (!InNetwork)
            this.Changes();

        GameObjectList firewallList = GameWorld.Find("firewallList") as GameObjectList;
        List<GameObject> firewalls = firewallList.Objects;
        foreach (Firewall firewall in firewalls)
        {
            if ((this.CollidesWith(firewall) && firewall.InCooldown == false && firewall.Placed))
            {
                GameEnvironment.AssetManager.PlaySound("Sounds/Flare");
                firewall.InCooldown = true;
                this.Changes();
            }

        }
    }

    public virtual void Changes()
    {
        delete = true;
    }

    public virtual void Fades()
    {
        this.Sprite.Alpha -= 10;

        if (this.Sprite.Alpha < 40)
            this.Changes();
    }

    public bool InNetwork
    {
        get
        {
            GameObjectList level = this.parent as GameObjectList;
            GameObjectList TowerList = level.Find("towerlist") as GameObjectList;
            List<GameObject> Towers = TowerList.Objects;
            foreach (Tower tower in Towers)
            {
                if (Math.Sqrt(Math.Pow((this.position.X - tower.Position.X - tower.Center.X), 2) + Math.Pow((this.position.Y - tower.Position.Y - tower.Center.Y), 2)) < 100||(Math.Sqrt(Math.Pow((this.position.X - serv.Position.X - serv.Center.X), 2) + Math.Pow((this.position.Y - serv.Position.Y - serv.Center.Y), 2)) < serv.Width))
                    return true;
            }
            return false;
        }
    }
}