using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

class LevelFinishedState : GameObjectList
{
    protected PlayingState playingState;
    SpriteGameObject star1, star2, star3;
    protected Button quitButton;

    public LevelFinishedState()
    {
        playingState = GameEnvironment.GameStateManager.GetGameState("playingState") as PlayingState;
        SpriteGameObject overlay = new SpriteGameObject("Sprites/Starbackground");
        star1 = new SpriteGameObject("Sprites/Star");
        star2 = new SpriteGameObject("Sprites/Star");
        star3 = new SpriteGameObject("Sprites/Star");

        overlay.Position = new Vector2(GameEnvironment.Screen.X, GameEnvironment.Screen.Y) / 2 - overlay.Center;
        star1.Position = overlay.Position + new Vector2(star1.Width / 2, star1.Height/5);
        star3.Position = overlay.Position + new Vector2(star2.Width * 3, star1.Height / 5);
        star2.Position = overlay.Position + new Vector2(overlay.Width/2 -50 , star1.Height / 5);

        this.Add(overlay);
        this.Add(star1);
        this.Add(star2);
        this.Add(star3);
        star2.Visible = false;
        star3.Visible = false;
        quitButton = new Button("Sprites/spr_button_quit", 100);
        quitButton.Position = new Vector2(GameEnvironment.Screen.X - quitButton.Width - 10, 10);
        this.Add(quitButton);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (quitButton.Pressed)
        {
            IGameLoopObject playingStatu = GameEnvironment.GameStateManager.CurrentGameState;
            playingState.Reset();
            this.Reset();
            GameEnvironment.GameStateManager.SwitchTo("levelMenu");
        }
        if (!inputHelper.KeyPressed(Keys.Space))
            return;
        GameEnvironment.GameStateManager.SwitchTo("playingState");
        playingState.Reset();
        (playingState as PlayingState).NextLevel();
    }

    public override void Update(GameTime gameTime)
    {
        playingState = GameEnvironment.GameStateManager.GetGameState("playingState") as PlayingState;
        Level level = playingState.CurrentLevel;
        UI ui = level.Find("ui") as UI;
        if (ui.Money > 100)
            star2.Visible = true;
        if (ui.Money >=
            300)
            star3.Visible = true;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        playingState.Draw(gameTime, spriteBatch);
        base.Draw(gameTime, spriteBatch);
    }
}