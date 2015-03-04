using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class PirateShip : SpriteGameObject
{
    bool inNetwork;
    double randomTimer;
    public PirateShip(Vector2 pos, int layer, String id)
        : base("Sprites/home", layer, id)
    {
        position = pos;
        this.Velocity = new Vector2(WifiWars.Random.Next(-50, 51), WifiWars.Random.Next(-50, 51));
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        inNetwork = false;
        randomTimer += gameTime.ElapsedGameTime.TotalSeconds;
        if (randomTimer > 10)
        {
            randomTimer = 0;
            this.Velocity = new Vector2(WifiWars.Random.Next(-50, 51), WifiWars.Random.Next(-50, 51));
        }   
        GameObjectList TowerList = GameWorld.Find("towerlist") as GameObjectList;
        List<GameObject> Towers = TowerList.Objects;
            foreach (Tower tower in Towers)
            {
                if (Math.Sqrt(Math.Pow((this.position.X - tower.Position.X - tower.Center.X), 2) + Math.Pow((this.position.Y - tower.Position.Y - tower.Center.Y), 2)) < 100)
                    inNetwork = true;

            }
            if (!inNetwork)
                velocity = -velocity;
    }
}
