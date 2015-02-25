using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

partial class Level : GameObjectList
{
    bool holdingTower;
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (quitButton.Pressed)
        {
            this.Reset();
            GameEnvironment.GameStateManager.SwitchTo("levelMenu");
        }
        UI ui = this.Find("ui") as UI;
        if (inputHelper.KeyPressed(Keys.T) && !holdingTower && ui.Money >= 20)
        {
            Tower tower = new Tower();
            this.Add(tower);
            holdingTower = true;
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

    public bool HoldingTower
    {
        get
        {
            return holdingTower;
        }
        set
        {
            holdingTower = value;
        }
    }
}
