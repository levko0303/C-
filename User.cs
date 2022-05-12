using System;
using System.Collections.Generic;
using System.Text;

namespace task3
{
    enum Role
    {
        admin, staff
    }
    abstract class USER
    {
        protected string first_name;
        protected string last_name;
        protected string email;
        protected string password;

        public abstract Role _Role
        { get; set; }

        public string First_name
        {
            get => first_name;
            set { first_name = Validation.valid_name(value); }
        }

        public string Last_name
        {
            get => last_name;
            set { last_name = Validation.valid_name(value); }
        }

        public string Email
        {
            get => email;
            set { email = Validation.valid_email(value); }
        }

        public string Password
        {
            get => password;
            set { password = value; }
        }
        public abstract void input();
        public abstract void see_status(Collection contract);
        public abstract void add_element(Collection contract);
        public abstract void delete_element(string email, Collection contract);
        public abstract void edit_element(string email, Collection contract);
        public abstract void change_send(Collection contract);
        public abstract void print_my_contracts(string email, Collection contract);
        public abstract void print_approved_contracts(Collection contract);
        public abstract void check_again(Collection contract);
        public abstract void edit_reject(string email, Collection contract);
    }

}
