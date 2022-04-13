using System;

namespace Lab_1
{

    class Program
    {
        static void menu()
        {
            Collection a = new Collection();
            Console.WriteLine("Input file name to save all changes which will be made with the collection: ");
            string file_name_wr = Input.input_file_name();
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
                            a.read_from_file();
                            a.write_in_file(file_name_wr);
                            a.print();
                            continue;
                        case 2:
                            string id1 = Input.input_id();
                            a.delete_by_id(id1);
                            a.write_in_file(file_name_wr);
                            continue;
                        case 3:
                            string id2 = Input.input_id();
                            a.edit_by_id(id2);
                            a.write_in_file(file_name_wr);
                            continue;
                        case 4:
                            string field = Input.field();
                            a.sort(field);
                            a.write_in_file(file_name_wr);
                            continue;
                        case 5:
                            Console.WriteLine("Input what you want to find: \n");
                            string condition = Console.ReadLine();
                            a.search(condition);
                            continue;
                        case 6:
                            a.add_element();
                            a.write_in_file(file_name_wr);
                            continue;
                        case 7:
                            a.print();
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
            menu();
        }
    }
}
