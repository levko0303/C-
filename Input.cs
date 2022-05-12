using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace task3
{
    class Input
    {

        public static string input_id()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Please input the id: \n");
                    int number = int.Parse(Console.ReadLine());
                    if (number <= 0)
                    {
                        Console.WriteLine("ID can't be negative: ");
                        continue;
                    }
                    return Convert.ToString(number);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

        }
        public static string input_file_name()
        {
            while (true)
                try
                {
                    string name = Console.ReadLine();
                    string path = "E:\\програмування\\task3\\task3";
                    string file_name = path + name;
                    if (!File.Exists(file_name))
                    {
                        Console.WriteLine("File doesn't exist");
                        continue;
                    }
                    return file_name;
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                }
        }

    }
}


