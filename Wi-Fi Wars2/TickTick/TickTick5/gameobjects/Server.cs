using System;
using System.IO;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

class Server : SpriteGameObject
{
    Color serverColor;
    double packetTimer;
    bool makePacket;
    public bool connected;
    int type;
    public bool spawnVirus, spawnTrojan;
    

    public Server(int color, Vector2 pos, int layer, String id, int virusLevel)
        : base("Sprites/Server", layer, id)
    {
        if (virusLevel > 0)
        {
            spawnVirus = true;
            if (virusLevel > 1)
                spawnTrojan = true;
        }
        else
        {
            spawnVirus = false;
            spawnTrojan = false;
        }

        position = pos;
        type = color;
        switch(color)
        {
            case 1:
                serverColor = Color.Red;         
                break;
            case 2:
                serverColor = Color.Purple;
                break;
            case 3:
                serverColor = Color.Yellow;
                break;
            default: throw new IOException("Invalid colour code: " + color);
        }
        sprite.SpriteColor = serverColor;
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (makePacket)
        {
            int rand = GameEnvironment.Random.Next(8);
            GameObjectList level = this.parent as GameObjectList;
            SpriteGameObject home = GameWorld.Find("home") as SpriteGameObject;
            GameObjectList TowerList = GameWorld.Find("towerlist") as GameObjectList;
            if (rand == 1 && spawnVirus == true)
            {
                Virus virus = new Virus(this.position + this.Center, this, TowerList, home);
                level.Add(virus);
            }
            else if (rand == 2 && spawnTrojan == true)
            {
                Trojan trojan = new Trojan(this.position + this.Center, this, TowerList, home);
                level.Add(trojan);
            }
            else
            {
                Packet packet = new Packet(this.position + this.Center, serverColor, type, this, TowerList, home);
                GameObjectList packetList = level.Find("packetList") as GameObjectList;
                packetList.Add(packet);
            }
            makePacket = false;
        }
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        packetTimer += gameTime.ElapsedGameTime.TotalSeconds;
        if (packetTimer > 2 && connected)
        {
            packetTimer = 0;
            makePacket = true;
        }
    }
    public void CheckConnection()
    {
        if (!connected)
        {
            GameObjectList towerList = GameWorld.Find("towerlist") as GameObjectList;
            List<GameObject> towers = towerList.Objects;
            foreach (Tower tower in towers)
                if (this.CollidesWith(tower) && tower.Connected)
                    this.connected = true;
        }
    }
}