﻿using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;

partial class Level : GameObjectList
{
    protected GameObjectList hideoutList;
    public void LoadLevel(string path)
    {
        hideoutList = new GameObjectList(0, "hideoutlist");
        this.Add(hideoutList);
        Vector2 position = Vector2.Zero;
        List<string> textlines = new List<string>();
        StreamReader fileReader = new StreamReader(path);
        string line = fileReader.ReadLine();
        while (line != null)
        {
            string[] obj = line.Split(' ');
            if(obj.Length >= 3)
                position = new Vector2(float.Parse(obj[1]), float.Parse(obj[2]));

            switch (obj[0])
            {
                case "server":
                    if (obj[3] != null)
                    {
                        this.Add(new Server(int.Parse(obj[3]), position, 1, "server" + obj[3]));
                        Bar serverBar = new Bar(int.Parse(obj[3]), "serverbar" + obj[3]);
                        ui.Add(serverBar);
                    }
                    else throw new IOException("Server objects needs 4 values, name, coords and colorcode(1-3)");
                    break;
                case "home":
                    this.Add(new Home(position, 1, "home"));
                    break;
                case "hideout": 
                    hideoutList.Add(new Hideout(position, 1, "hideout"));
                    break;
                case "antipirate": break;
                case "money":
                    ui.Money = int.Parse(obj[1]);
                    break;
                default: throw new IOException("Unknown object type: " + obj[0]);
            }

            line = fileReader.ReadLine();
        }
        fileReader.Close();
    }

}