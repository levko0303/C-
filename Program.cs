using System;
using System.Collections.Generic;

namespace task3
{
    class Program
    {
        static void menu()
        {
            Collection contract = new Collection();
            contract.read_from_file("E:\\програмування\\task3\\task3\\contracts.txt");
            Actions action = new Actions();
            bool check = true;
            USER current_user;
            while (check)
                try
                {
                    Console.WriteLine("\n Choose what you want:\n" +
                  "1 - registation\n" +
                  "2 - log in\n" +
                  "3 - stop program\n");
                    int what_shosen = int.Parse(Console.ReadLine());
                    action.read_from_file();
                    switch (what_shosen)
                    {
                        case 1:
                            action.registration();
                            continue;
                        case 2:
                            current_user = action.log();
                            if (current_user == null)
                            {
                                Console.WriteLine("Please restart session");
                                break;
                            }
                            bool check1 = true;
                            while (check1)
                            {
                                try
                                {
                                    string email = current_user.Email;
                                    if (current_user._Role == Role.staff)
                                    {
                                        Console.WriteLine("Choose what you want:\n" +
                                      "1 - create contract\n" +
                                      "2 - delete contract from collection by ID\n" +
                                      "3 - edit contract in collection by ID\n" +
                                      "4 - print my added contracts \n" +
                                      "5 - change contract and send it to moderation\n" +
                                      "6 - print all aded contracts with status Approved\n" +
                                      "7 - exit\n");
                                        int what_shosen1 = int.Parse(Console.ReadLine());
                                        switch (what_shosen1)
                                        {
                                            case 1:
                                                current_user.add_element(contract);
                                                continue;
                                            case 2:
                                                current_user.delete_element(email, contract);
                                                continue;
                                            case 3:
                                                current_user.edit_element(email, contract);
                                                continue;
                                            case 4:
                                                current_user.print_my_contracts(email, contract);
                                                continue;
                                            case 5:
                                                current_user.change_send(contract);
                                                current_user.edit_reject(email, contract);
                                                continue;
                                            case 6:
                                                current_user.print_approved_contracts(contract);
                                                continue;
                                            case 7:
                                                check1 = false;
                                                break;
                                            default:
                                                Console.WriteLine("Enter right option");
                                                break;
                                        }
                                    }
                                    else if (current_user._Role == Role.admin)
                                    {
                                        Console.WriteLine("\n Start observing contracts... \n");
                                        Console.WriteLine("Choose what you want:\n" +
                                      "1 - change status or see contracts\n" +
                                      "2 - exit\n");
                                        int what_shosen1 = int.Parse(Console.ReadLine());
                                        switch (what_shosen1)
                                        {
                                            case 1:
                                                current_user.see_status(contract);
                                                continue;
                                            case 2:
                                                check1 = false;
                                                break;
                                            default:
                                                Console.WriteLine("Enter right option");
                                                break;
                                        }
                                        break;
                                    }
                                    else
                                        break;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                }
                            }
                            continue;
                        case 3:
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
