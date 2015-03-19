using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class AntiPirateHideout : SpriteGameObject
{
    public bool connected;
    double pirateTimer;
    bool makeAntiPirate;
    public AntiPirateHideout(Vector2 pos, int layer, String id)
        : base("Sprites/Hideout", layer, id)
    {
        position = pos;
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (makeAntiPirate)
        {
            GameObjectList level = this.parent.Parent as GameObjectList;
            AntiPirate antiPirate = new AntiPirate(position, 1010, "antipirate");
            level.Add(antiPirate);
            makeAntiPirate = false;
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
            makeAntiPirate = true;
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