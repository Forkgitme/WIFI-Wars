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
            PirateShip pirate = new PirateShip(position + new Vector2(70,50), 1010, "pirate");
            GameObjectList pirateList = level.Find("pirateList") as GameObjectList;
            pirateList.Add(pirate);
            makePirate = false;
        }
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (connected)
            pirateTimer += gameTime.ElapsedGameTime.TotalSeconds;
        if (pirateTimer > 4)
        {
            pirateTimer = 0;
            makePirate = true;
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