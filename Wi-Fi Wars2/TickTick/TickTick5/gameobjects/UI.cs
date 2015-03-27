using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

class UI : GameObjectList
{
    public bool spawnVirus, done;
    float money;
    TextGameObject moneyText;
    public UI(int m, int layer = 0, string id = "ui")
        : base(layer, id)
    {
        money = m;
        moneyText = new TextGameObject("Fonts/Hud");
        moneyText.Position = new Vector2(450, 20);
        moneyText.Color = Color.Black;
        this.Add(moneyText);
        Bar policeBar = new Bar(4, "police");
        this.Add(policeBar);
        SpriteGameObject buffer = new SpriteGameObject("Sprites/Buffer", 10, "buffer");
        buffer.Position = new Vector2(250, 15);
        this.Add(buffer);
        SpriteGameObject towerBase = new SpriteGameObject("Sprites/tower", 10);
        towerBase.Position = new Vector2(750 - towerBase.Width/2, 105 - towerBase.Height/2);
        this.Add(towerBase);
        SpriteGameObject towerRange = new SpriteGameObject("Sprites/Range", 9);
        towerRange.Position = new Vector2(750 - towerRange.Width/2, 105 - towerRange.Height/2);
        this.Add(towerRange);
    }


    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (spawnVirus && !done)
            for (int i = 0; i < 6; i++)
            {
                SpriteGameObject fireWall = new SpriteGameObject("Sprites/firewall");
                fireWall.Position = new Vector2(900, 30 + i * 30 - fireWall.Height / 2);
                this.Add(fireWall);
                done = true;
            }
        moneyText.Text = "Money:" + (int)money + "$";    
    }
    public float Money
    {
        get { return money; }
        set { money = value; }
    }
}
