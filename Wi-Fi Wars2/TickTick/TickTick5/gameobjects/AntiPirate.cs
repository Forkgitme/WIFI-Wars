using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class AntiPirate : AnimatedGameObject
{
    bool inNetwork;
    double randomTimer; 
    bool remove;
    int outsidetimeout;

    public AntiPirate(Vector2 pos, int layer, String id)
        : base(layer, "antipirate")
    {
        
        position = pos;
        Vector2 randomVector = new Vector2(WifiWars.Random.Next(-100, 51), WifiWars.Random.Next(-100, 101));
        randomVector.Normalize();
        this.velocity = randomVector * 50;
        this.LoadAnimation("Sprites/AntiPirate@2","sailing",true, 0.1f);
        this.PlayAnimation("sailing");
      
    }

   
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (remove)
        {
            GameObjectList parent = this.parent as GameObjectList;
            parent.Remove(this);
        }
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
        GameObjectList level = this.parent as GameObjectList;
        GameObjectList TowerList = level.Find("towerlist") as GameObjectList;
        List<GameObject> Towers = TowerList.Objects;
            foreach (Tower tower in Towers)
            {
                if (Math.Sqrt(Math.Pow((this.position.X - tower.Position.X - tower.Center.X), 2) + Math.Pow((this.position.Y - tower.Position.Y - tower.Center.Y), 2)) < 100)
                    inNetwork = true;

            }
            if (!inNetwork)
            {
                velocity = -velocity;
                outsidetimeout += 1;
            }
            if (outsidetimeout >= 5)
                remove = true;
            GameObjectList pirateList = level.Find("pirateList") as GameObjectList;
            List<GameObject> pirates = pirateList.Objects;
        foreach (PirateShip pirate in pirates)
        {
            if (Math.Sqrt(Math.Pow((this.position.X - pirate.Position.X - pirate.Center.X), 2) + Math.Pow((this.position.Y - pirate.Position.Y - pirate.Center.Y), 2)) < 150)
            {
                Vector2 direction = new Vector2(pirate.Position.X - this.position.X, pirate.Position.Y - this.position.Y);
                direction.Normalize();
                this.Velocity = direction * 75;
            }
            if (this.CollidesWith(pirate))
            {
                pirate.remove = true;
                remove = true;
            }
        }
    }
}
