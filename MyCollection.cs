using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Lab_1
{
    class Collection<T> where T : new()
    {
        private List<T> _array = new List<T>();
        List<T> array
        {
            get { return _array; }
            set { _array = value; }
        }

        public void print()
        {
            foreach (T el in array)
            {
                Console.WriteLine(el.ToString());
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
                        T element = new T();
                        string[] data = line.Split("  ");
                        for (int num_of_field = 0; num_of_field < data.Count(); num_of_field++)
                        {
                            var arr_of_properties = element.GetType().GetProperties();
                            var property = arr_of_properties[num_of_field];
                            if (arr_of_properties[num_of_field].PropertyType == typeof(DateTime))
                                property.SetValue(element, Validation.convert_to_date(data[num_of_field]));
                            else
                                property.SetValue(element, Convert.ChangeType(data[num_of_field], arr_of_properties[num_of_field].PropertyType));
                        }
                        array.Add(element);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void add_element(T element)
        {
            try
            {
                Console.WriteLine("Please, add new element \n");
                array.Add(element);
                Console.WriteLine("The element was added \n");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void delete_by_id(string id)
        {
            bool check = false;
            for (int i = 0; i < array.Count; i++)
            {
                var arr_of_properties = array[i].GetType().GetProperties();
                if (id == arr_of_properties[0].GetValue(array[i]).ToString())
                {
                    check = true;
                    array.Remove(array[i]);
                    Console.WriteLine("Element with id " + id + " was deleted");
                }
            }
            if (check == false)
            {
                Console.WriteLine("There are no element with such id");
            }

        }

        public void edit_by_id(string id)
        {
            bool check = false;
            for (int i = 0; i < array.Count; i++)
            {
                var arr_of_properties = array[i].GetType().GetProperties();
                if (id == arr_of_properties[0].GetValue(array[i]).ToString())
                {
                    try
                    {
                        T element = new T();
                        Console.WriteLine("To edit the object input new values with a comma: ");
                        string new_values = Console.ReadLine();

                        string[] array_of_new_values = new_values.Split(", ");
                        for (int j = 0; j < array_of_new_values.Length; j++)
                        {
                            Console.WriteLine(array_of_new_values[j]);
                            var property = arr_of_properties[j];
                            property.SetValue(element, Convert.ChangeType(array_of_new_values[j], arr_of_properties[j].PropertyType));
                        }
                        array[i] = element;
                        Console.WriteLine("\nThe element was edited");
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
                T element = new T();
                if (element.GetType().GetProperty(field) == null)
                {
                    Console.WriteLine("\nThere are no such field");
                }
                else
                {
                    array = array.OrderBy(condition => condition.GetType().GetProperty(field).GetValue(condition)).ToList();
                    Console.WriteLine("\nThe collection was sorted");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void search(string str_to_find)
        {
            bool search_pred(T element)
            {
                foreach (PropertyInfo property in element.GetType().GetProperties())
                    if (property.GetValue(element).ToString().Contains(str_to_find))
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
                    Console.WriteLine(result_of_search[i]);
            }
        }
        public void write_in_file(string file_name)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(file_name, false))
                {
                    foreach (T element in array)
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
