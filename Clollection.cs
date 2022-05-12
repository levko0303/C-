using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace task3
{
    class Collection
    {

        private List<Contract> _contracts_list = new List<Contract>();
        List<Contract> contracts_list
        {
            get { return _contracts_list; }
            set { _contracts_list = value; }
        }

        public void print()
        {
            foreach (Contract el in contracts_list)
            {
                Console.WriteLine(el.ToString());
            }
        }
        public Contract add_element()
        {
            try
            {
                Contract element = new Contract();
                Console.WriteLine("Please, add new contract \n");
                element.input();
                contracts_list.Add(element);
                element.status = Status.Draft;
                Console.WriteLine("\nThe element was added\n");
                return element;
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                return new Contract();
            }
        }

        public void edit_element(string email)
        {

            string id = Input.input_id();
            bool check = false;
            for (int i = 0; i < contracts_list.Count; i++)
            {
                var arr_of_properties = contracts_list[i].GetType().GetProperties();
                if (id == arr_of_properties[0].GetValue(contracts_list[i]).ToString())
                {
                    if (contracts_list[i].status == Status.Draft && contracts_list[i].user_email == email)
                    {
                        try
                        {
                            check = true;
                            Contract element = new Contract();
                            element.id = contracts_list[i].id;
                            element.user_email = contracts_list[i].user_email;
                            element.input();
                            contracts_list[i] = element;
                            Console.WriteLine("\nThe element was edited");
                            write_in_file("E:\\програмування\\task3\\task3\\contracts.txt");
                        }
                        catch (ArgumentException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                    else
                    {
                        check = true;
                        Console.WriteLine($"You don't have enough permission => Status: {contracts_list[i].status}, User: --- ");
                    }

                }
            }
            if (check == false)
                Console.WriteLine("There are no contract with such id\n");
        }
        public void delete_element(string email)
        {
            string id = Input.input_id();
            bool check = false;
            for (int i = 0; i < contracts_list.Count; i++)
            {
                if (id == contracts_list[i].id)
                {
                    if (contracts_list[i].status == Status.Draft && contracts_list[i].user_email == email)
                    {
                        contracts_list.Remove(contracts_list[i]);
                        Console.WriteLine("Contract with id " + id + " was deleted");
                        write_in_file("E:\\програмування\\task3\\task3\\contracts.txt");
                    }
                    else
                    {
                        check = true;
                        Console.WriteLine($"You don't have enough permission => Status: {contracts_list[i].status}, User: --- ");
                    }
                }
            }
            if (check == false)
            {
                Console.WriteLine("There are no contract with such id");
            }

        }

        public void edit_reject(string email)
        {
            string id = Input.input_id();
            bool check = false;
            for (int i = 0; i < contracts_list.Count; i++)
            {
                var arr_of_properties = contracts_list[i].GetType().GetProperties();
                if (id == arr_of_properties[0].GetValue(contracts_list[i]).ToString())
                {
                    if (contracts_list[i].status == Status.Rejected && contracts_list[i].user_email == email)
                    {
                        try
                        {
                            check = true;
                            Contract element = new Contract();
                            element.id = contracts_list[i].id;
                            element.user_email = contracts_list[i].user_email;
                            element.input();
                            element.status = Status.Draft;
                            element.Message = contracts_list[i].Message;
                            contracts_list[i] = element;
                            Console.WriteLine("\nThe element was edited");
                            write_in_file("E:\\програмування\\task3\\task3\\contracts.txt");
                        }
                        catch (ArgumentException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                    else
                    {
                        check = true;
                        Console.WriteLine($"You don't have enough permission => Status: {contracts_list[i].status}, User: --- ");
                    }

                }
            }
            if (check == false)
                Console.WriteLine("There are no contract with such id\n");
        }

        public void print_my_contracts(string email)
        {
            Console.WriteLine("Select status contracts which you want to see:\n" +
               "1 - Draft\n" +
               "2 - Rejected\n" +
               "3 - Approved\n");
            int what_chosen1 = int.Parse(Console.ReadLine());

            if (what_chosen1 == 1)
            {
                for (int i = 0; i < contracts_list.Count; i++)
                {
                    if (contracts_list[i].status == Status.Draft && contracts_list[i].user_email == email)
                    {
                        Console.WriteLine(contracts_list[i].ToString());
                    }
                }
            }
            else if (what_chosen1 == 2)
            {
                for (int i = 0; i < contracts_list.Count; i++)
                {
                    if (contracts_list[i].status == Status.Rejected && contracts_list[i].user_email == email)
                    {
                        Console.WriteLine(contracts_list[i].ToString());
                    }
                }
            }
            else
            {
                for (int i = 0; i < contracts_list.Count; i++)
                {
                    if (contracts_list[i].status == Status.Approved && contracts_list[i].user_email == email)
                    {
                        Console.WriteLine(contracts_list[i].ToString());
                    }
                }
            }

        }

        public void change_stat()
        {
            Console.WriteLine("\nYou want to change status:");
            string id = Input.input_id();

            Console.WriteLine("Select status:\n" +
                       "1 - Approved\n" +
                       "2 - Rejected\n" +
                       "3 - Don't change\n");
            bool check = false;
            int what_chosen = int.Parse(Console.ReadLine());
            for (int i = 0; i < contracts_list.Count; i++)
            {
                var arr_of_properties = contracts_list[i].GetType().GetProperties();
                if (id == arr_of_properties[0].GetValue(contracts_list[i]).ToString())
                {
                    if (what_chosen == 1)
                    {
                        check = true;
                        Contract element = new Contract();
                        element = contracts_list[i];
                        element.status = Status.Approved;
                        element.Message = null;
                        contracts_list[i] = element;
                        break;
                    }
                    else if (what_chosen == 2)
                    {
                        check = true;
                        Contract element1 = new Contract();
                        element1 = contracts_list[i];
                        element1.status = Status.Rejected;
                        element1.Message = Class_Validation.valid_message(Console.ReadLine());
                        contracts_list[i] = element1;
                        break;
                    }
                }
            }
            if (check == false)
                Console.WriteLine("\nSomething  went wrong. There are no such id or status contracts");
        }
        public void print_approved_contracts()
        {
            for (int i = 0; i < contracts_list.Count; i++)
            {
                if (contracts_list[i].status == Status.Approved)
                {
                    Console.WriteLine(contracts_list[i].ToString());
                }
            }
        }

        public void check_again()
        {
            bool check = false;
            for (int i = 0; i < contracts_list.Count; i++)
            {
                if (contracts_list[i].status == Status.Rejected && contracts_list[i].Message.EndsWith("DONE"))
                {
                    Console.WriteLine(contracts_list[i]);
                    check = true;
                }
            }
            if (check == false)
            {
                Console.WriteLine("\n There are no changed contracts yet");
            }
            else
                change_stat();
            write_in_file("E:\\програмування\\task3\\task3\\contracts.txt");

        }
        public void filter_contracts(int what_chosen)
        {
            if (what_chosen == 1)
            {
                for (int i = 0; i < contracts_list.Count; i++)
                {
                    if (contracts_list[i].status == Status.Draft)
                    {
                        Console.WriteLine(contracts_list[i].ToString());
                    }
                }
            }
            else if (what_chosen == 2)
            {
                for (int i = 0; i < contracts_list.Count; i++)
                {
                    if (contracts_list[i].status == Status.Rejected)
                    {
                        Console.WriteLine(contracts_list[i].ToString());
                    }
                }
            }
            else
            {
                for (int i = 0; i < contracts_list.Count; i++)
                {
                    if (contracts_list[i].status == Status.Approved)
                    {
                        Console.WriteLine(contracts_list[i].ToString());
                    }
                }
            }
        }

        public void see_status()
        {
            bool check = true;
            while (check == true)
            {
                Console.WriteLine("Select status contracts which you want to see. If you want to stop session select 4:\n" +
                "1 - Draft\n" +
                "2 - Rejected\n" +
                "3 - Approved\n" +
                "4 - Turn back to the main page \n");

                int what_chosen1 = int.Parse(Console.ReadLine());
                if (what_chosen1 == 1)
                {
                    filter_contracts(what_chosen1);
                    change_stat();
                }
                else if (what_chosen1 == 2)
                {
                    filter_contracts(what_chosen1);
                }
                else if (what_chosen1 == 3)
                {
                    filter_contracts(what_chosen1);
                }
                else
                {
                    check = false;
                }
                write_in_file("E:\\програмування\\task3\\task3\\contracts.txt");
            }

        }


        public void read_from_file(string file_name)
        {
            try
            {
                using (StreamReader sr = new StreamReader(file_name))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Contract element = new Contract();
                        string[] data = line.Split("  ");
                        for (int num_of_field = 0; num_of_field < data.Count(); num_of_field++)
                        {
                            var arr_of_properties = element.GetType().GetProperties();
                            var property = arr_of_properties[num_of_field];
                            if (arr_of_properties[num_of_field].PropertyType == typeof(DateTime))
                                property.SetValue(element, Class_Validation.convert_to_date(data[num_of_field]));
                            else if (num_of_field == data.Count() - 2)
                                property.SetValue(element, Enum.Parse<Status>(data[num_of_field]));
                            else
                                property.SetValue(element, Convert.ChangeType(data[num_of_field], arr_of_properties[num_of_field].PropertyType));
                        }
                        contracts_list.Add(element);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void write_in_file(string file_name)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(file_name, false))
                {
                    foreach (Contract element in contracts_list)
                    {
                        var properties = element.GetType().GetProperties();
                        int num_of_fields = properties.Length;
                        for (int i = 0; i < num_of_fields; i++)
                        {
                            var property = properties[i];
                            sw.Write(property.GetValue(element));
                            if (i == num_of_fields - 1)
                                sw.Write("");
                            else
                                sw.Write("  ");
                        }
                        sw.Write("\n");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
