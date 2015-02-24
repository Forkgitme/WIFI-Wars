using Microsoft.Xna.Framework;

partial class Level : GameObjectList
{
    protected bool locked, solved;
    protected Button quitButton;

    public Level(int levelIndex)
    {
        quitButton = new Button("Sprites/spr_button_quit", 100);
        quitButton.Position = new Vector2(GameEnvironment.Screen.X - quitButton.Width - 10, 10);
        this.Add(quitButton);
        SpriteGameObject background = new SpriteGameObject("Backgrounds/Background" + levelIndex, 0, "background");
        this.Add(background);
    }

    public bool Completed
    {
        get
        {    
            return false;
        }
    }

    public bool GameOver
    {
        get
        {
            return false;
        }
    }

    public bool Locked
    {
        get { return locked; }
        set { locked = value; }
    }

    public bool Solved
    {
        get { return solved; }
        set { solved = value; }
    }
}

