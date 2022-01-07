using System;
using System.Collections.Generic;

namespace AsciiE
{
    public class Obj
    {
        public string name;         // Name of the object
        public string location;     // Location of the object

        public string type;         // User defined type
        public string data;         // User defined data; not implemented fully, could be used for scripting...
        public string att;          // Attributes like 'not-passable'

        public int x;               // X position of this object
        public int y;               // Y position of this object

        
        // Optional constructor
        public Obj(string name, string location, string type)
        {
            this.name = name;
            this.location = location;
            this.type = type;
        }

        public Obj() { }


        // Load from file/string
        public void Load(string text)
        {
            // Seperated by Colons (:)
            string[] split = text.Split(':');
           
            // Name/Location
            name = split[0];
            location = split[1];
            // Type/Data
            type = split[2];
            data = split[3];
            // Attributes
            att = split[4];
            // X/Y position
            x = int.Parse(split[5]);
            y = int.Parse(split[6]);
        }

        // Save to a string (to be stored in a text file)
        public string Save()
        {
            return name + ":" + location + ":" + type + ":" + data + ":" + att + ":" + x + ":" + y;
        }
        
        // Add data to objects data, in the form of bytes.
        public void AddData(byte[] newdata)
        {
            foreach(byte b in newdata)
            {
                this.data += (char)b;   
            }
        }
        
        // Add data to objects data, in the form of characters
        public void AddData(string newdata)
        {
            foreach(char c in newdata)
            {
                this.data += c;   
            }
        }

        // Contains this attribute
        public bool HasAttribute(string attribute)
        {
            // Check all the attributes (seperated by commas (,))
            foreach(string s in att.Split(','))
            {
                // Comparing
                if (s == attribute)
                    return true;
            }

            return false;
        }

        // Add and attribute
        public void AddAttribute(string attribute)
        {
            // Add comma
            if (att != "")
                att += ",";
            
            // Add to attributes
            att += attribute;
        }
        
        // Remove an attribute
        public void RemoveAttribute(string attribute)
        {
            // Collect into a list so we can remove one
            List<string> split = new List<string>();
            // Add each attribute
            foreach(string s in att.Split(',')) { split.Add(s); }

            // Attribute to remove (if it's here)
            int rem = -1;
            
            // Check to see if it's here
            for (int i = 0; i < split.Count; i++)
            {
                // find the sttribute to remove
                if(split[i] == attribute)
                {
                    rem = i;
                    break;
                }
            }


            // If it's here remove it
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
