using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;

partial class Level : GameObjectList
{
    public void LoadLevel(string path)
    {
        List<string> textlines = new List<string>();
        StreamReader fileReader = new StreamReader(path);
        string line = fileReader.ReadLine();
        while (line != null)
        {
            string[] obj = line.Split(' ');
            Vector2 position = new Vector2(float.Parse(obj[1]), float.Parse(obj[2]));

            switch (obj[0])
            {
                case "server":
                    if (obj[4] != null)
                    {
                        this.Add(new Server(int.Parse(obj[4]), position, 1, "server" + obj[4]));
                    }
                    else throw new IOException("Server objects needs 4 values, name, coords and colorcode(1-3)");
                    break;
                case "home": break;
                case "hideout": break;
                case "antipirate": break;
                default: throw new IOException("Unknown object type: " + obj[0]);
            }

            line = fileReader.ReadLine();
        }
        fileReader.Close();
    }

}