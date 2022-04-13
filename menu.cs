using System;
using System.IO;

namespace Lab_1
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
                    string path = "E:\\навчальна практика\\programlab1\\programlab1\\";
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

        public static string field()
        {

            Console.WriteLine("Choose what you want:\n" +
                  "1 - ID \n" +
                  "2 - contractor_name\n" +
                  "3 - contractor_email\n" +
                  "4 - contractor_phone\n" +
                  "5 - contractor_iban\n" +
                  "6 - start_date\n" +
                  "7 - due_date\n");
            int number = int.Parse(Console.ReadLine());
            switch (number)
            {
                case 1:
                    return "id";
                case 2:
                    return "contractor_name";
                case 3:
                    return "contractor_email";
                case 4:
                    return "contractor_phone";
                case 5:
                    return "contractor_iban";
                case 6:
                    return "start_date";
                case 7:
                    return "due_date";
            }
            return "Such field doesn't exist";

        }
    }
}
