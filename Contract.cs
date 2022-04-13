using System;

namespace Lab_1
{
    class Contract
    {
        private string _id;
        private string _contractor_name;
        private string _contractor_email;
        private string _contractor_phone;
        private string _contractor_iban;
        private DateTime _start_date;
        private DateTime _due_date;

        public string id
        {
            get { return _id; }
            set { _id = Validation.valid_id(value); }
        }


        public string contractor_name
        {
            get { return _contractor_name; }
            set { _contractor_name = Validation.valid_name(value); }
        }

        public string contractor_email
        {
            get { return _contractor_email; }
            set { _contractor_email = Validation.check_email(value); }
        }

        public string contractor_phone
        {
            get { return _contractor_phone; }
            set { _contractor_phone = Validation.valid_phone(value); }
        }

        public string contractor_iban
        {
            get { return _contractor_iban; }
            set { _contractor_iban = Validation.valid_iban(value); }
        }

        public DateTime start_date
        {
            get { return _start_date; }
            set { _start_date = value; }
        }

        public DateTime due_date
        {
            get { return _due_date; }
            set { _due_date = Validation.compare_dates(start_date, value); }
        }


        public string print()
        {
            return ($"ID: {id} \nContractor name: {contractor_name} \nContractor email: {contractor_email} " +
                $"\nContractor phone: {contractor_phone} \nContractor iban: {contractor_iban} " +
                $"\nStart date: {start_date} \nDue date: {due_date}\n"); ;
        }

        public void input()
        {
            Console.Write("Id: ");
            id = Console.ReadLine();
            Console.Write("Contractor name: ");
            contractor_name = Console.ReadLine();
            Console.Write("contractor_email: ");
            contractor_email = Console.ReadLine();
            Console.Write("Contractor phone: ");
            contractor_phone = Console.ReadLine();
            Console.Write("Contractor iban: ");
            contractor_iban = Console.ReadLine();
            Console.Write("Start date: ");
            start_date = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Due date: ");
            due_date = Convert.ToDateTime(Console.ReadLine());
        }


    }
}