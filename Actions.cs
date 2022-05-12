using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace task3
{
    class Actions
    {
        private List<USER> _array = new List<USER>();
        private USER current_user;
        private string path = "E:\\програмування\\task3\\task3\\users.txt";
        List<USER> array
        {
            get { return _array; }
            set { _array = value; }
        }
        public void registration()
        {
            try
            {
                Console.WriteLine("-----Registration form----- \n");
                Staff element = new Staff();
                element.input();
                if (element.Email != " " && element.Password != " " && element.Last_name != " " && element.First_name != " ")
                {
                    element.First_day_in_company = Validation.date_now();
                    element.Password = Validation.hash_passwd(element.Password.ToString());
                    element._Role = Role.staff;
                    array.Add(element);
                    var type = element.GetType();
                    var properties = TypeDescriptor.GetProperties(type);
                    int num_of_fields = properties.Count;

                    for (int i = 0; i < num_of_fields - 1; i++)
                    {
                        var property = properties[i];
                        File.AppendAllText(path, property.GetValue(element) + " ");
                        if (i == num_of_fields - 2)
                        {
                            property = properties[num_of_fields - 1];
                            File.AppendAllText(path, property.GetValue(element) + Environment.NewLine);
                        }
                    }
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public USER log()
        {
            Console.WriteLine("----- Please log in ----- \n");
            Console.Write("Email: ");
            string email = Validation.valid_email(Console.ReadLine());
            Console.Write("Password: ");
            string password = Validation.valid_passwd();
            bool run_once = false;
            for (int i = 0; i < array.Count(); i++)
            {
                if (array[i].Email.ToString() == email)
                {
                    if (array[i].Password == Validation.hash_passwd(password))
                    {
                        run_once = true;
                        current_user = array[i];
                        current_user._Role = array[i]._Role;
                        Console.WriteLine("\nCurrent user: ");
                        Console.WriteLine($"{current_user.First_name} {current_user.Last_name}\nRole: {current_user._Role} \n");
                        break;
                    }
                }
            }
            if (run_once == false)
            {
                Console.WriteLine("\nYour data are not correct or maybe you are not registered yet\n");
                current_user = null;
            }
            return current_user;
        }

        public void read_from_file()
        {
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Staff member = new Staff();
                        var type = member.GetType();
                        Admin mem = new Admin();
                        var tp = mem.GetType();

                        string[] data = line.Split(" ");

                        if (data.Count() == 7)
                        {
                            for (int num_of_field = 0; num_of_field < data.Count(); num_of_field++)
                            {

                                var arr_of_properties = TypeDescriptor.GetProperties(type);
                                var property = arr_of_properties[num_of_field];
                                if (num_of_field == 1)
                                    property.SetValue(member, Validation.convert_to_date(data[num_of_field]));
                                else if (num_of_field == 2)
                                {
                                    property.SetValue(member, Enum.Parse<Role>(data[num_of_field]));
                                }
                                else
                                    property.SetValue(member, Convert.ChangeType(data[num_of_field], arr_of_properties[num_of_field].PropertyType));
                            }
                            array.Add(member);
                        }
                        if (data.Count() == 5)
                        {
                            for (int num_of_field = 0; num_of_field < data.Count(); num_of_field++)
                            {
                                var arr_of_properties = TypeDescriptor.GetProperties(tp);
                                var property = arr_of_properties[num_of_field];
                                if (num_of_field == 0)
                                {
                                    property.SetValue(mem, Enum.Parse<Role>(data[num_of_field]));
                                }
                                else
                                    property.SetValue(mem, Convert.ChangeType(data[num_of_field], arr_of_properties[num_of_field].PropertyType));
                            }
                            array.Add(mem);
                        }
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







