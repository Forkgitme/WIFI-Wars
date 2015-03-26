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
    bool active;
    public AntiPirateHideout(Vector2 pos, int layer, String id)
        : base("Sprites/AntiPirateHideout", layer, id)
    {
        position = pos;
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (connected)
        {
            if (inputHelper.MousePosition.X < this.Position.X + 64 && inputHelper.MousePosition.X > this.Position.X + 28 && inputHelper.MousePosition.Y < this.Position.Y + 15 && inputHelper.MousePosition.Y > this.Position.Y)
                    if (inputHelper.MouseLeftButtonPressed())
                    {
                        if (!active)
                        { 
                            this.Sprite = new SpriteSheet("Sprites/AntiPirateHideoutActive", 0);
                            active = true;
                        }
                        else
                        {
                            this.Sprite = new SpriteSheet("Sprites/AntiPirateHideout", 0);
                            active = false;
                        }
                    }     
        }
        if (makeAntiPirate)
        {
            GameObjectList level = this.parent.Parent as GameObjectList;
            AntiPirate antiPirate = new AntiPirate(position + new Vector2(45,40), 1010, "antipirate");
            level.Add(antiPirate);
            makeAntiPirate = false;
        }
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (active)
        {
            GameObjectList level = this.parent.Parent as GameObjectList;
            UI ui = level.Find("ui") as UI;
            if (ui.Money >= 0.01)
            ui.Money -= 0.01f;
            else
            {
                this.Sprite = new SpriteSheet("Sprites/AntiPirateHideout", 0);
                active = false;
                GameEnvironment.AssetManager.PlaySound("Sounds/snd_switch");
            }
            pirateTimer += gameTime.ElapsedGameTime.TotalSeconds;
        }
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