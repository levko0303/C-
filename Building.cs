using System;

namespace Lab_1
{
    class Building
    {
        private string _id;
        private string name;
        private string email;
        private string post_index;
        private string _contractor_iban;
        private DateTime _start_date;
        private DateTime _due_date;

        public string id
        {
            get { return _id; }
            set { _id = Validation.valid_id(value); }
        }


        public string Name
        {
            get { return name; }
            set { name = Validation.valid_name(value); }
        }

        public string Email
        {
            get { return email; }
            set { email = Validation.check_email(value); }
        }

        public string Index
        {
            get { return post_index; }
            set { post_index = Validation.valid_phone(value); }
        }
        public string contractor_iban
        {
            get { return _contractor_iban; }
            set { _contractor_iban = Validation.valid_iban(value); }
        }
        public DateTime start_date_living
        {
            get { return _start_date; }
            set { _start_date = value; }
        }

        public DateTime due_date_living
        {
            get { return _due_date; }
            set { _due_date = Validation.compare_dates(start_date_living, value); }
        }


        public override string ToString()
        {
            return ($"ID: {id} \nname: {name} \nemail: {email} " +
                $"\nContractor phone: {post_index} " +
                $"\nStart date: {start_date_living} \nDue date: {due_date_living}\n"); ;
        }

        public void input()
        {
            Console.Write("Id: ");
            id = Console.ReadLine();
            Console.Write("Contractor name: ");
            name = Console.ReadLine();
            Console.Write("contractor_email: ");
            email = Console.ReadLine();
            Console.Write("Contractor phone: ");
            post_index = Console.ReadLine();
            Console.Write("Contractor iban: ");
            contractor_iban = Console.ReadLine();
            Console.Write("Start date: ");
            start_date_living = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Due date: ");
            due_date_living = Convert.ToDateTime(Console.ReadLine());
        }


    }
}