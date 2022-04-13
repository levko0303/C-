using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace Lab_1
{
    class Collection
    {
        private List<Contract> _array = new List<Contract>();
        List<Contract> array
        {
            get { return _array; }
            set { _array = value; }
        }

        public void print()
        {
            for (int i = 0; i < array.Count; i++)
                Console.WriteLine(array[i].print());
        }
        public void read_from_file()
        {
            try
            {
                Console.WriteLine("Input file name to read data from: \n");
                string file_name = Input.input_file_name();
                using (StreamReader sr = new StreamReader(file_name))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Contract member = new Contract();
                        string[] data = line.Split(" ");
                        var type = member.GetType();
                        for (int num_of_field = 0; num_of_field < data.Count(); num_of_field++)
                        {
                            var arr_of_properties = TypeDescriptor.GetProperties(type);
                            var property = arr_of_properties[num_of_field];
                            if (num_of_field >= data.Count() - 2 && num_of_field <= data.Count() - 1)
                                property.SetValue(member, Validation.convert_to_date(data[num_of_field]));
                            else
                                property.SetValue(member, data[num_of_field]);
                        }
                        array.Add(member);
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void delete_by_id(string id)
        {
            bool check = false;
            for (int i = 0; i < array.Count; i++)
            {
                if (id == array[i].id)
                {
                    check = true;
                    array.Remove(array[i]);
                    Console.WriteLine("Contract with id " + id + " was deleted");
                }
            }
            if (check == false)
            {
                Console.WriteLine("There are no contract with such id");
            }

        }


        public void add_element()
        {
            try
            {
                Contract element = new Contract();
                Console.WriteLine("Please, add new contract \n");
                element.input();
                array.Add(element);
                Console.WriteLine("The element was added\n");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }

       

 

        public void edit_by_id(string id)
        {
            bool check = false;
            for (int i = 0; i < array.Count; i++)
            {
                if (array[i].id == id)
                {
                    try
                    {
                        Contract element = new Contract();
                        element.input();
                        array[i] = element;
                        check = true;
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            if (check == false)
                Console.WriteLine("There are no contract with such id\n");
        }

        public void sort(string field)
        {
            try
            {
                array = array.OrderBy(condition => condition.GetType().GetProperty(field).GetValue(condition)).ToList();
                Console.WriteLine("The collection was sorted");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        public void search(string str_to_find)
        {
            bool search_pred(Contract member)
            {
                foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(member))
                    if (property.GetValue(member).ToString().Contains(str_to_find))
                    {
                        return true;
                    }
                return false;
            }
            var result_of_search = array.FindAll(search_pred);
            if (result_of_search.Count == 0)
            {
                Console.WriteLine("No matches found");
            }
            else
            {
                for (int i = 0; i < result_of_search.Count; i++)
                    Console.WriteLine(result_of_search[i].print());
            }
        }

        

        public void write_in_file(string file_name)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(file_name, false))
                {
                    foreach (Contract member in array)
                    {
                        var type = member.GetType();
                        var properties = TypeDescriptor.GetProperties(type);
                        int num_of_fields = properties.Count;
                        for (int i = 0; i < num_of_fields; i++)
                        {
                            var property = properties[i];
                            sw.Write(property.GetValue(member));
                            sw.Write(" ");
                        }
                        sw.Write("\n");
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

}