using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

partial class Level : GameObjectList
{
    bool barsfilled;
    bool holding;
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (quitButton.Pressed)
        {
            IGameLoopObject playingState = GameEnvironment.GameStateManager.CurrentGameState;
            playingState.Reset();
            this.Reset();
         

            GameEnvironment.GameStateManager.SwitchTo("levelMenu");
        }
        UI ui = this.Find("ui") as UI;
        if ((inputHelper.MouseLeftButtonPressed() && (Math.Sqrt((inputHelper.MousePosition.X - 750) * (inputHelper.MousePosition.X - 750) + (inputHelper.MousePosition.Y - 105)*(inputHelper.MousePosition.Y - 105)) < 100) || inputHelper.KeyPressed(Keys.T)) && !holding && ui.Money >= 20)
        {
            Tower tower = new Tower();
            towerList.Add(tower);
            holding = true;
            ui.Money -= 20;
        }
        if ((inputHelper.MouseLeftButtonPressed() && inputHelper.MousePosition.X > 900 && inputHelper.MousePosition.X <= 1000 && inputHelper.MousePosition.Y > 16 && inputHelper.MousePosition.Y < 190) || inputHelper.KeyPressed(Keys.F) && !holding && ui.Money >= 20)
        {
            Firewall firewall = new Firewall();
            firewallList.Add(firewall);
            holding = true;
            ui.Money -= 20;
        }
    }

    public override void Update(GameTime gameTime)
    {
        barsfilled = true;
        for (int i = 1; i < 4; i++)
        {
            Bar bar = this.Find("serverbar" + i) as Bar;

            if( bar != null)
                if(bar.Resource < bar.TotalResource)
                    barsfilled = false;
            
             
        }

        if (barsfilled)
        {
            this.solved = true;
            GameEnvironment.GameStateManager.SwitchTo("levelFinishedState");
        }

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
