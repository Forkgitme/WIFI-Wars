using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Hideout : SpriteGameObject
{
    public bool connected;
    double pirateTimer;
    bool makePirate;
    int pirateCount;
    public Hideout(Vector2 pos, int layer, String id)
        : base("Sprites/Hideout", layer, id)
    {
        position = pos;
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (makePirate)
        {
            GameObjectList level = this.parent.Parent as GameObjectList;
            PirateShip pirate = new PirateShip(position, 10, "pirate");
            level.Add(pirate);
            makePirate = false;
            pirateCount += 1;
        }
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (pirateCount < 2)
        { 
        pirateTimer += gameTime.ElapsedGameTime.TotalSeconds;
        if (pirateTimer > 10 && connected)
        {
            pirateTimer = 0;
            makePirate = true;
        }
        }
    }
    public void CheckConnection()
    {
        if (!connected)
        {
            GameObjectList towerList = GameWorld.Find("towerlist") as GameObjectList;
            List<GameObject> towers = towerList.Objects;
            foreach (Tower tower in towers)
                if (this.CollidesWith(tower) && tower.Connected)
                    this.connected = true;
        }
    }
}