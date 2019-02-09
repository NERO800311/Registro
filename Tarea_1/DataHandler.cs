using System.Collections.Generic;

namespace Tarea_1
{
    public class DataHandler
    {
        private Dictionary<string, string> items;
        private List<string> xx;

        public static DataHandler instance = new DataHandler();

        private DataHandler()
        {
            items = new Dictionary<string, string>();
        }

        public void Add(string data)
        {

        }

        public void ToStream(string key, string data)
        {

        }
    }
}