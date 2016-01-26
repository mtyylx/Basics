using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    class Hash
    {
        public Object UseHashTable()
        {
            //Not Generic. Key and Value are stored in Object. No type defined in declaration.
            Hashtable hash = new Hashtable();

            //You are free to store any kind of data into it.
            hash.Add("some string", 111);
            hash.Add(1234, 222);
            hash.Add(new int[]{123, 234}, 333);

            //When extract Value by Key, you have to cast it from Object. This is painful.
            int findKeyValue = (int) hash["some string"];

            //Return Null if key not found.
            return hash["invalid key"];
        }

        public int UseDictionary()
        {
            //Generic. Key and Value type is predefined and saved in its original type. Type Safe.
            Dictionary<string, int> dict = new Dictionary<string, int>();

            //You are only allowed to store the defined type into it.
            dict.Add("Only string", 111);
            dict.Add("Can be", 222);
            dict.Add("Added to", 333);
            dict.Add("The Dict", 444);

            //When extract Value by Key, you don't worry about the output type or casting.
            int findKeyValue = dict["Only string"];

            //Traversal the Dictionary is easy, because data type are uniformed.
            foreach (KeyValuePair<string, int> kvp in dict)
            {
                Console.WriteLine("KVP = " + kvp);
            }

            foreach (string key in dict.Keys)
            {
                Console.WriteLine("Key = " + key);
            }

            foreach (int value in dict.Values)
            {
                Console.WriteLine("Value = " + value);
            }

            //Throw KeyNotFoundException if key not found.
            return (int)dict["invalid key"];
        }
    }
}
