using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

class UI : GameObjectList
{
    int money;
    TextGameObject moneyText;
    public UI(int m, int layer = 0, string id = "ui")
        : base(layer, id)
    {
        money = m;
        moneyText = new TextGameObject("Fonts/Hud");
        moneyText.Position = new Vector2(50, 50);
        this.Add(moneyText);
        Bar policeBar = new Bar(4, "police");
        this.Add(policeBar);
        SpriteGameObject buffer = new SpriteGameObject("Sprites/Buffer", 10, "buffer");
        buffer.Position = new Vector2(600, 50);
        this.Add(buffer);
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        moneyText.Text = "Money:" + money + "$";
    }
    public int Money
    {
        get { return money; }
        set { money = value; }
    }
}
