using System;
using System.IO;

namespace Tarea_1
{
    public delegate void Backend (object data);
    public class Utils
    {
        public static Backend log = data  => Console.WriteLine(data);
        public static Backend error = data => Console.WriteLine("Error: {0}", data);

        public static void touch(string name)
        {
            StreamWriter file = File.AppendText(name);
            file.Close();
            file = null;
        }
        
        //Parche por fallos entre linux y windows
        public static float parse(string number)
        {
            if (number.Contains(","))
            {
                float num1 = float.Parse(number);
                float num2 = float.Parse(number.Replace(",", "."));
                if (num1 > num2) return num2;
            }

            if (number.Contains("."))
            {
                float num1 = float.Parse(number);
                float num2 = float.Parse(number.Replace(".", ","));
                if (num1 > num2) return num2;
            }

            return float.Parse(number);
        }

    }
}