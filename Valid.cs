using System;


namespace Lab_1
{
    class Validation
    {


        public static string valid_id(string value)
        {
            try
            {
                if (int.Parse(value) <= 0)
                {
                    Console.WriteLine($"ID can't be negative: {value}");
                }
                return Convert.ToString(value);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return " ";
            }

        }


        public static string valid_name(string value)
        {
            bool check = true;
            try
            {
                foreach (char ch in value)
                {
                    if (!char.IsLetter(ch))
                        check = false;
                }
                if (!check)
                    Console.WriteLine($"Name is incorrect: {value}");
                return value;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return " ";
            }
        }



        public static string valid_phone(string value)
        {
            try
            {
                if (!value.StartsWith("+380") || value.Length != 13)
                    Console.WriteLine($"The format of phone is incorrect: {value}");
                else
                    foreach (char ch in value)
                    {
                        if (char.IsLetter(ch))
                            Console.WriteLine($"The format of phone is incorrect: {value}");
                    }
                return value;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return " ";
            }
        }

       

        public static string valid_iban(string value)
        {
            bool check1 = true;
            bool check2 = true;
            try
            {
                string code = value[0..2];
                string digit = value[2..];
                if (value.Length == 29)
                {
                    foreach (char ch in code)
                    {
                        if (!char.IsLetter(ch))
                            check1 = false;
                    }

                    foreach (char ch in digit)
                    {
                        if (!char.IsNumber(ch))
                            check2 = false;
                    }
                }
                else
                    check1 = check2 = false;
                if (check1 == false || check2 == false)
                    Console.WriteLine($"Iban is incorrect: {value}");
                return value;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return " ";
            }
        }

       
        public static DateTime compare_dates(DateTime value1, DateTime value2)
        {
            try
            {
                if (DateTime.Compare(value1, value2) > 0)
                    Console.WriteLine($"Start date {value1} can't be bigger than the due date { value2}");

                return value2;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new DateTime();
            }

        }

        public static DateTime convert_to_date(string s)
        {
            try
            {
                if (!DateTime.TryParse(s, out DateTime result))
                {
                    Console.WriteLine($"Wrong date: {s}");
                    return new DateTime();
                }
                else
                    return Convert.ToDateTime(s);

            }
            catch
            {
                return new DateTime();
            }
        }

        public static string check_email(string value)
        {

            try
            {
                bool check = System.Text.RegularExpressions.Regex.IsMatch(value, @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
                if (!check)
                    Console.WriteLine($"Wrong email: {value}");
                return value;
            }
            catch
            {
                Console.WriteLine($"Wrong email: {value}");
                return " ";
            }
        }


    }
}