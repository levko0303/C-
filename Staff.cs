using System;
using System.Collections.Generic;
using System.Text;

namespace task3
{
    class Staff : USER
    {

        private double salary;
        private string first_day_in_company;
        public double Salary
        {
            get => salary;
            set { salary = Validation.valid_salary(value); }
        }

        public string First_day_in_company
        {
            get => first_day_in_company;
            set { first_day_in_company = Validation.convert_to_date(value); }
        }

        public override Role _Role
        { get; set; } = Role.staff;

        public override string ToString()
        {
            return ($"First name: {First_name} \nLast name: {Last_name} " +
                $"\nEmail: {Email} \nPassword: {Password} " +
                $"\nSalary: {salary} \nFirst day in company: {first_day_in_company}" +
                $"\nRole: {_Role} \n");
        }

        public override void input()
        {
            Console.Write("First name: ");
            First_name = Console.ReadLine();
            Console.Write("Last name: ");
            Last_name = Console.ReadLine();
            Console.Write("Email: ");
            Email = Console.ReadLine();
            Console.Write("Password: ");
            Password = Validation.valid_passwd();

        }

        public override void change_send(Collection contract)
        {
            contract.filter_contracts(2);
        }
        public static void read_file(Collection contract)
        {
            contract.read_from_file("E:\\програмування\\task3\\task3\\contracts.txt");
        }

        public override void print_my_contracts(string email, Collection contract)
        {
            contract.print_my_contracts(email);
        }
        public override void print_approved_contracts(Collection contract)
        {
            contract.print_approved_contracts();
        }
        public override void delete_element(string email, Collection contract)
        {
            contract.delete_element(email);
        }

        public override void edit_element(string email, Collection contract)
        {
            contract.edit_element(email);
        }

        public override void edit_reject(string email, Collection contract)
        {
            contract.edit_reject(email);
        }

        public override void add_element(Collection contract)
        {
            Contract element = new Contract();
            element = contract.add_element();
            element.user_email = Email;
            contract.write_in_file("E:\\програмування\\task3\\task3\\contracts.txt");

        }
        public override void see_status(Collection contract) { }
        public override void check_again(Collection contract) { }

    }
}
