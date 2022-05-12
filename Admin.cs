using System;
using System.Collections.Generic;

namespace task3
{
    class Admin : USER
    {
        public override Role _Role
        { get; set; } = Role.admin;
        public override string ToString()
        {
            return ($"First name: {First_name} \nLast name: {Last_name} " +
                $"\nEmail: {Email} \nPassword: {Password} " +
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
        public override void print_approved_contracts(Collection contract) { }
        public override void see_status(Collection contract)
        {
            contract.see_status();

        }

        public override void check_again(Collection contract)
        {
            contract.check_again();
        }

        public override void add_element(Collection contract) { }
        public override void delete_element(string email, Collection contract) { }
        public override void edit_element(string email, Collection contract) { }
        public override void print_my_contracts(string email, Collection contract) { }
        public override void change_send(Collection contract) { }
        public override void edit_reject(string email, Collection contract) { }
    }
}
