using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

partial class Level : GameObjectList
{
    bool holding;
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (quitButton.Pressed)
        {
            this.Reset();
            GameEnvironment.GameStateManager.SwitchTo("levelMenu");
        }
        UI ui = this.Find("ui") as UI;
        if (inputHelper.KeyPressed(Keys.T) && !holding && ui.Money >= 20)
        {
            Tower tower = new Tower();
            towerList.Add(tower);
            holding = true;
            ui.Money -= 20;
        }
        if (inputHelper.KeyPressed(Keys.F) && !holding && ui.Money >= 20)
        {
            Firewall firewall = new Firewall();
            this.Add(firewall);
            holding = true;
            ui.Money -= 20;
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public override void Reset()
    {
        base.Reset();
    }

    public bool Holding
    {
        get
        {
            return holding;
        }
        set
        {
            holding = value;
        }
    }
}
