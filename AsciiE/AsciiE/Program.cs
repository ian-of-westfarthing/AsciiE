using System;

namespace AsciiE
{
    class MainClass
    {
        public static float Version = 0.12f;// Version of AsciiE

        public static void Main(string[] args)
        {
            
            // Loading message
            Console.WriteLine("AsciiE version " + Version);
            Console.WriteLine("Loading...");
            
            // add default block (air)
            if(args.Length <= 1)
            {
                Engine.Objects.Add(new Obj("Nothing", "", "air"));
                Engine.Objects[0].AddAttribute("passable");
            }
            
            // Load from file / new file
            if (args.Length == 0)
            {
                Engine.Edit();
            }
            else if (args.Length > 1)
                Engine.Load(args[1]);

            // Play or Edit the data
            if (args[0] == "play")
            {
                Engine.Run();
            }
            else if (args[0] == "edit")
            {
                Engine.Edit();
            }
        }
    }
}
