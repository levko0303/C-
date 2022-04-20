using System;
using System.Collections.Generic;

namespace Lab_1
{
    class Program
    {
        static void choice()
        {
            try
            {
                Console.WriteLine("Choose a collection to work with: \n");
                Console.WriteLine(
                        "1 - Contract\n" +
                        "2 - Transaction\n");
                int choice = Convert.ToInt32((Console.ReadLine()));
                if (choice == 1)
                    menu1();
                else if (choice == 2)
                    menu2();
                else
                    Console.WriteLine("Select option with is put above");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void menu1()
        {
            Collection<Contract> contr1 = new Collection<Contract>();
            Console.WriteLine("Input file name to read data from: \n");
            string file_name = Input.input_file_name();
            bool check = true;
            while (check)
                try
                {
                    Console.WriteLine("Choose what you want:\n" +
                  "1 - read data from file into collection\n" +
                  "2 - delete element from collection by ID\n" +
                  "3 - edit element in collection by ID\n" +
                  "4 - sort contracts \n" +
                  "5 - search information in contracts\n" +
                  "6 - add new element into container and file \n" +
                  "7 - print contracts \n" +
                  "8 - exit\n");


                    int what_shosen = int.Parse(Console.ReadLine());
                    switch (what_shosen)
                    {
                        case 1:
                            contr1.read_from_file(file_name);
                            contr1.print();
                            continue;
                        case 2:
                            string id1 = Input.input_id();
                            contr1.delete_by_id(id1);
                            contr1.write_in_file(file_name);
                            continue;
                        case 3:
                            string id2 = Input.input_id();
                            contr1.edit_by_id(id2);
                            contr1.write_in_file(file_name);
                            continue;
                        case 4:
                            string field = Input.field();
                            contr1.sort(field);
                            contr1.write_in_file(file_name);
                            continue;
                        case 5:
                            Console.WriteLine("Input what you want to find: \n");
                            string condition = Console.ReadLine();
                            contr1.search(condition);
                            continue;
                        case 6:
                            Contract con = new Contract();
                            con.input();
                            contr1.add_element(con);
                            contr1.write_in_file(file_name);
                            continue;
                        case 7:
                            contr1.print();
                            continue;
                        case 8:
                            check = false;
                            break;
                        default:
                            Console.WriteLine("Enter right option");
                            break;

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
        }

        static void menu2()
        {
            Collection<Building> contr1 = new Collection<Building>();
            Console.WriteLine("Input file name to read data from: \n");
            string file_name = Input.input_file_name();
            bool check = true;
            while (check)
                try
                {
                    Console.WriteLine("Choose what you want:\n" +
                    "1 - read data from file into collection\n" +
                    "2 - delete element from collection by ID\n" +
                    "3 - edit element in collection by ID\n" +
                    "4 - sort contracts \n" +
                    "5 - search information in contracts\n" +
                    "6 - add new element into container and file \n" +
                    "7 - print contracts \n" +
                    "8 - exit\n");


                    int what_shosen = int.Parse(Console.ReadLine());
                    switch (what_shosen)
                    {
                        case 1:
                            contr1.read_from_file(file_name);
                            contr1.print();
                            continue;
                        case 2:
                            string id1 = Input.input_id();
                            contr1.delete_by_id(id1);
                            contr1.write_in_file(file_name);
                            continue;
                        case 3:
                            string id2 = Input.input_id();
                            contr1.edit_by_id(id2);
                            contr1.write_in_file(file_name);
                            continue;
                        case 4:
                            string field = Input.field();
                            contr1.sort(field);
                            contr1.write_in_file(file_name);
                            continue;
                        case 5:
                            Console.WriteLine("Input what you want to find: \n");
                            string condition = Console.ReadLine();
                            contr1.search(condition);
                            continue;
                        case 6:
                            Building con = new Building();
                            con.input();
                            contr1.add_element(con);
                            contr1.write_in_file(file_name);
                            continue;
                        case 7:
                            contr1.print();
                            continue;
                        case 8:
                            check = false;
                            break;
                        default:
                            Console.WriteLine("Enter right option");
                            break;

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

        }
        static void Main(string[] args)
        {
            choice();
        }
    }
}
