using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

class GameOverState : GameObjectList
{
    protected IGameLoopObject playingState;
    protected Button quitButton, retryButton;

    public GameOverState()
    {
        playingState = GameEnvironment.GameStateManager.GetGameState("playingState");
        SpriteGameObject overlay = new SpriteGameObject("Sprites/GameOver");
        overlay.Position = new Vector2(GameEnvironment.Screen.X, GameEnvironment.Screen.Y) / 2 - overlay.Center;
        this.Add(overlay);
        quitButton = new Button("Sprites/spr_button_quit", 100);
        quitButton.Position = new Vector2(GameEnvironment.Screen.X - quitButton.Width - 10, 10);
        this.Add(quitButton);
        retryButton = new Button("Sprites/spr_button_retry", 100);
        retryButton.Position = new Vector2(GameEnvironment.Screen.X - quitButton.Width - 10, 70);
        this.Add(retryButton);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (retryButton.Pressed)
        {
            GameEnvironment.GameStateManager.SwitchTo("playingState");
            IGameLoopObject playingState = GameEnvironment.GameStateManager.CurrentGameState;
            playingState.Reset();
        }
        if (quitButton.Pressed)
        {
            IGameLoopObject playingState = GameEnvironment.GameStateManager.CurrentGameState;
            playingState.Reset();
            this.Reset();
            GameEnvironment.GameStateManager.SwitchTo("levelMenu");
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        playingState.Draw(gameTime, spriteBatch);
        base.Draw(gameTime, spriteBatch);
    }
}