using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

class WifiWars : GameEnvironment
{
    static void Main()
    {
        WifiWars game = new WifiWars();
        game.Run();
    }

    public WifiWars()
    {
        Content.RootDirectory = "Content";
        this.IsMouseVisible = true;
    }

    protected override void LoadContent()
    {
        base.LoadContent();

        screen = new Point(1440, 825);
        this.SetFullScreen(false);

        gameStateManager.AddGameState("titleMenu", new TitleMenuState());
        gameStateManager.AddGameState("helpState", new HelpState());
        gameStateManager.AddGameState("playingState", new PlayingState(Content));
        gameStateManager.AddGameState("levelMenu", new LevelMenuState());
        gameStateManager.AddGameState("gameOverState", new GameOverState());
        gameStateManager.SwitchTo("titleMenu");
    }
}