using System;
using System.Collections.Generic;

namespace AsciiE
{
    public class Obj
    {
        public string name;         // Name of the object
        public string location;     // Location of the object

        public string type;         // User defined type
        public string data;         // User defined data
        public string att;          // Attributes like 'not-passable'

        public int x;
        public int y;


        public Obj(string name, string location, string type)
        {
            this.name = name;
            this.location = location;
            this.type = type;
        }

        public Obj() { }


        // Load from text
        public void Load(string text)
        {
            string[] split = text.Split(':');

            name = split[0];
            location = split[1];
            type = split[2];
            data = split[3];
            att = split[4];
            x = int.Parse(split[5]);
            y = int.Parse(split[6]);
        }

        // Save from text
        public string Save()
        {
            return name + ":" + location + ":" + type + ":" + data + ":" + att + ":" + x + ":" + y;
        }

        // Contains this attribute
        public bool HasAttribute(string attribute)
        {
            foreach(string s in att.Split(','))
            {
                if (s == attribute)
                    return true;
            }

            return false;
        }

        // Add/Remove
        public void AddAttribute(string attribute)
        {
            if (att != "")
                att += ",";

            att += attribute;
        }

        public void RemoveAttribute(string attribute)
        {
            List<string> split = new List<string>();

            foreach(string s in att.Split(',')) { split.Add(s); }

            int rem = -1;

            for (int i = 0; i < split.Count; i++)
            {
                // find the sttribute to remove
                if(split[i] == attribute)
                {
                    rem = i;
                    break;
                }
            }


            // It must be done
            if (rem != -1)
            {
                split.RemoveAt(rem);

                att = "";

                // 'recompile' it
                for(int i = 0; i < split.Count; i++)
                {
                    if (i != 0)
                        att += ",";

                    att += split[i];
                }
            }
        }
    }
}
