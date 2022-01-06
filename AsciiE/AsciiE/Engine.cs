using System;
using System.Collections.Generic;
using System.IO;

namespace AsciiE
{
    public static class Engine
    {
        // Variables
        public static float projectVersion = 1.00f;
        public static string projectName = "";
        public static ProjectType projectType = ProjectType.rogueLike;
        public static List<Obj> Objects = new List<Obj>();

        // Player position
        // In-game type resgame to reset everything
        public static int playerX = 0;
        public static int playerY = 0;

        // Play the game
        public static void Run()
        {
            bool isRunning = true;
            Console.WriteLine("-- " + projectName + " v" + projectVersion + " --");

            while (isRunning)
            {
                Console.Write(">");

                string input = Console.ReadLine();

                switch (input.Split()[0])
                {
                    case "walk":
                        string dir = input.Split()[1];

                        switch (dir)
                        {
                            case "left":
                                if (Passable(playerX - 1, playerY))
                                {
                                    playerX--;
                                }
                                else
                                {
                                    Console.WriteLine("You cannot go there!");
                                }
                                break;
                            case "right":
                                if (Passable(playerX + 1, playerY))
                                {
                                    playerX++;
                                }
                                else
                                {
                                    Console.WriteLine("You cannot go there!");
                                }
                                break;
                            case "for":
                                if (Passable(playerX, playerY - 1))
                                {
                                    playerY--;
                                }
                                else
                                {
                                    Console.WriteLine("You cannot go there!");
                                }
                                break;
                            case "back":
                                if (Passable(playerX, playerY + 1))
                                {
                                    playerY++;
                                }
                                else
                                {
                                    Console.WriteLine("You cannot go there!");
                                }
                                break;
                            default:
                                Console.WriteLine("Unknown Direction.");
                                break;
                        }
                        break;
                    case "look":
                        Obj here = GetObj(playerX, playerY);

                        if(here.type == "air")
                        {
                            Console.WriteLine("you see nothing here");
                        }
                        else
                        {
                            Console.WriteLine("you see " + here.name + " a " + here.type + " at " + here.location);
                        }
                        break;
                    case "quit":
                        Save();// Overwrites the game files, (include a copy?)
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Bad Command.");
                        break;
                }
            }
        }

        // Game functions:
        public static bool Passable(int x, int y)
        {
            foreach(Obj obj in Objects)
            {
                if(obj.x == x && obj.y == y)
                {
                    //  'Tisn't passable!
                    if (obj.HasAttribute("not-passable"))
                    {
                        return false;
                    }
                    break;
                }
            }

            return true;
        }

        // Get object at x/y
        public static Obj GetObj(int x, int y)
        {
            foreach (Obj obj in Objects)
            {
                if (obj.x == x && obj.y == y)
                {
                    return obj;
                }
            }

            return null;
        }

        // Get object by name
        public static Obj GetObj(string name)
        {
            foreach (Obj obj in Objects)
            {
                if (obj.name == name)
                {
                    return obj;
                }
            }

            return null;
        }

        // Edit the game
        public static void Edit()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.Write("$ ");

                string input = Console.ReadLine();

                try
                {
                    switch (input.Split()[0])
                    {
                        case "cls":
                            Console.Clear();
                            break;
                        case "clear":
                            Console.Clear();
                            break;
                        case "save":
                            Save();
                            break;
                        case "load":
                            Load(input.Split()[1]);
                            break;
                        case "name":
                            projectName = input.Split()[1];
                            break;
                        case "ver":
                            projectVersion = float.Parse(input.Split()[1]);
                            break;
                        case "obj":
                            string[] split = input.Split();

                            if (split[1] == "new")
                            {
                                Obj obj = new Obj(split[2], split[3], split[4]);
                                Objects.Add(obj);
                            }
                            else if (split[1] == "edit")
                            {
                                Obj obj = GetObj(split[2]);

                                if (obj != null)
                                {
                                    switch (split[3])
                                    {
                                        case "name":
                                            string newName = split[4];
                                            bool Valid = true;

                                            foreach (Obj _obj in Objects)
                                            {
                                                if (_obj.name == newName)
                                                {
                                                    Valid = false;
                                                    break;
                                                }
                                            }

                                            if (Valid)
                                            {
                                                foreach (Obj _obj in Objects)
                                                {
                                                    if (_obj.name == obj.name)
                                                    {
                                                        _obj.name = newName;
                                                        Console.WriteLine("Success!");
                                                        break;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Invalid Name.");
                                            }
                                            break;
                                        case "location":
                                            foreach (Obj _obj in Objects)
                                            {
                                                if (_obj.name == obj.name)
                                                {
                                                    _obj.location = split[4];
                                                    obj.location = split[4];
                                                    break;
                                                }
                                            }
                                            break;
                                        case "type":
                                            foreach (Obj _obj in Objects)
                                            {
                                                if (_obj.name == obj.name)
                                                {
                                                    _obj.type = split[4];
                                                    obj.type = split[4];
                                                    break;
                                                }
                                            }
                                            break;
                                        case "pos":
                                            foreach (Obj _obj in Objects)
                                            {
                                                if (_obj.name == obj.name)
                                                {
                                                    _obj.x = int.Parse(split[4]);
                                                    _obj.y = int.Parse(split[5]);
                                                    obj.x = int.Parse(split[4]);
                                                    obj.y = int.Parse(split[5]);
                                                    break;
                                                }
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Object Not Found.");
                                }
                            }
                            else if (split[1] == "delete")
                            {
                                string name = split[2];

                                foreach (Obj obj in Objects)
                                {
                                    if (obj.name == name)
                                    {
                                        Objects.Remove(obj);
                                        break;
                                    }
                                }
                            }
                            else if (split[1] == "att")
                            {
                                string name = split[2];

                                switch (split[3])
                                {
                                    case "add":
                                        foreach(Obj obj in Objects)
                                        {
                                            if(obj.name == name)
                                            {
                                                obj.AddAttribute(split[4]);
                                            }
                                        }
                                        break;
                                    case "remove":
                                        foreach (Obj obj in Objects)
                                        {
                                            if (obj.name == name)
                                            {
                                                obj.RemoveAttribute(split[4]);
                                            }
                                        }
                                        break;
                                }
                            }
                            break;
                    }
                }
                catch
                {
                    Console.WriteLine("* ERROR");
                }

            }
        }

        // Load assets
        public static void Load(string fileName)
        {
            string[] file = File.ReadAllLines(fileName);

            projectName = file[0];
            projectType = (ProjectType)int.Parse(file[1]);
            playerX = int.Parse(file[3]);
            playerY = int.Parse(file[4]);
            projectVersion = float.Parse(file[5]);

            // Load in all the objects (from a seperate file)
            string[] objs = File.ReadAllLines(file[2]);

            foreach(string s in objs)
            {
                Obj obj = new Obj();
                obj.Load(s);

                Objects.Add(obj);
            }
        }

        // Save assets
        public static void Save()
        {
            string[] file = new string[6];

            file[0] = projectName;
            file[1] = ((int)projectType).ToString();

            file[2] = projectName + "-obj";
            file[3] = playerX.ToString();
            file[4] = playerY.ToString();
            file[5] = projectVersion;

            File.WriteAllLines(projectName, file);

            // OBJECTS ----------
            string[] objs = new string[Objects.Count];

            for (int i = 0; i < objs.Length; i++)
            {
                objs[i] = Objects[i].Save();
            }

            File.WriteAllLines(projectName + "-obj", objs);
        }
    }
}
