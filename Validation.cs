using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Globalization;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace task3
{
    class Validation
    {

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
                if ((!check) || (value.Length < 2))
                {
                    Console.WriteLine($"Name is incorrect: {value}");
                    return " ";
                }
                else
                    return value;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return " ";
            }
        }

        public static string valid_email(string value)
        {

            try
            {
                bool check = Regex.IsMatch(value, @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
                if (!check)
                {
                    Console.WriteLine($"Wrong email: {value}");
                    return " ";
                }
                else
                    return value;
            }
            catch
            {
                Console.WriteLine($"Wrong email: {value}");
                return " ";
            }
        }

        public static SecureString maskInputString()
        {
            SecureString pass = new SecureString();
            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = Console.ReadKey(true);
                if (!char.IsControl(keyInfo.KeyChar))
                {
                    pass.AppendChar(keyInfo.KeyChar);
                    Console.Write("*");
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    pass.RemoveAt(pass.Length - 1);
                    Console.WriteLine("\b \b");
                }
            }
            while (keyInfo.Key != ConsoleKey.Enter);
            {
                return pass;
            }
        }

        public static string show_password()
        {
            SecureString pass = maskInputString();
            string Password = new System.Net.NetworkCredential(string.Empty, pass).Password;
            return Password;
        }
        public static string valid_passwd()
        {
            try
            {
                string value = show_password();
                var hasNumber = new Regex(@"[0-9]+");
                var hasUpperChar = new Regex(@"[A-Z]+");
                var hasMinimum8Chars = new Regex(@".{8,}");
                bool check = hasNumber.IsMatch(value) && hasUpperChar.IsMatch(value) && hasMinimum8Chars.IsMatch(value);
                if (!check)
                {
                    Console.WriteLine($"\n Wrong password");
                    return " ";
                }
                else
                    return value;

            }
            catch
            {
                Console.WriteLine($"Wrong password");
                return " ";
            }
        }

        public static string hash_passwd(string value)
        {
            string hashed = " ";
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(value));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                hashed = builder.ToString();
            }
            return hashed;
        }

        public static double valid_salary(double value)
        {
            try
            {
                if (value < 0)
                {
                    Console.WriteLine($"Salary can't be negative: {value}");
                    return 0;
                }
                return Convert.ToDouble(value);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }

        }

        public static string convert_to_date(string s)
        {
            try
            {
                var date = DateTime.ParseExact(s, "dd.MM.yyyy",
                                   CultureInfo.InvariantCulture);
                return date.ToString("dd.MM.yyyy");
            }
            catch
            {
                Console.WriteLine("Wrong date");
                return "00/00/0000";
            }
        }


        public static string date_now()
        {
            return DateTime.Now.ToString("dd/MM/yyyy");
        }


    }
}
