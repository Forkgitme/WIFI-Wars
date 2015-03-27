using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

class GameOverState : GameObjectList
{
    protected IGameLoopObject playingState;
    protected Button retryButton;

    public GameOverState()
    {
        playingState = GameEnvironment.GameStateManager.GetGameState("playingState");
        SpriteGameObject overlay = new SpriteGameObject("Sprites/GameOver");
        overlay.Position = new Vector2(GameEnvironment.Screen.X, GameEnvironment.Screen.Y) / 2 - overlay.Center;
        this.Add(overlay);

        retryButton = new Button("Sprites/spr_button_retry", 3);
        retryButton.Position = new Vector2((GameEnvironment.Screen.X - retryButton.Width) / 2, 625);
        this.Add(retryButton);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (inputHelper.KeyPressed(Keys.Space) || retryButton.Pressed)
        {
            GameEnvironment.GameStateManager.SwitchTo("playingState");
            IGameLoopObject playingState = GameEnvironment.GameStateManager.CurrentGameState;
            playingState.Reset();
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        playingState.Draw(gameTime, spriteBatch);
        base.Draw(gameTime, spriteBatch);
    }
}