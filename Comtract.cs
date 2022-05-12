using System;

namespace task3
{
    enum Status
    {
        Draft, Approved, Rejected
    }
    class Contract
    {
        private string _id;
        private string _user_email;
        private string _contractor_name;
        private string _contractor_email;
        private string _contractor_phone;
        private string _contractor_iban;
        private DateTime _start_date;
        private DateTime _due_date;
        private string _message;

        public string id
        {
            get { return _id; }
            set { _id = Class_Validation.valid_id(value); }
        }

        public string user_email
        {
            get { return _user_email; }
            set { _user_email = Class_Validation.valid_email(value); }
        }
        public string contractor_name
        {
            get { return _contractor_name; }
            set { _contractor_name = Class_Validation.valid_name(value); }
        }

        public string contractor_email
        {
            get { return _contractor_email; }
            set { _contractor_email = Class_Validation.valid_email(value); }
        }

        public string contractor_phone
        {
            get { return _contractor_phone; }
            set { _contractor_phone = Class_Validation.valid_phone(value); }
        }

        public string contractor_iban
        {
            get { return _contractor_iban; }
            set { _contractor_iban = Class_Validation.valid_iban(value); }
        }

        public DateTime start_date
        {
            get { return _start_date; }
            set { _start_date = value; }
        }

        public DateTime due_date
        {
            get { return _due_date; }
            set { _due_date = Class_Validation.compare_dates(start_date, value); }
        }

        public Status status
        { get; set; }

        public string Message
        {
            get => _message;
            set
            {
                if (this.status == Status.Approved || this.status == Status.Draft)
                    _message = null;
                else
                    _message = Class_Validation.valid_message(value);
            }
        }


        public override string ToString()
        {
            if (this.status == Status.Approved || this.status == Status.Draft)
                return ($"ID: {id} \nContractor name: {contractor_name} \nContractor email: {contractor_email} " +
                    $"\nContractor phone: {contractor_phone} \nContractor iban: {contractor_iban} " +
                    $"\nStart date: {start_date} \nDue date: {due_date} " +
                    $"\nStatus: {status} \n");
            else
                return ($"ID: {id} \nContractor name: {contractor_name} \nContractor email: {contractor_email} " +
                    $"\nContractor phone: {contractor_phone} \nContractor iban: {contractor_iban} " +
                    $"\nStart date: {start_date} \nDue date: {due_date} " +
                    $"\nStatus: {status} \nMessage {Message}\n");
        }

        public void input()
        {
            Console.Write("Id: ");
            id = Console.ReadLine();
            Console.Write("Contractor name: ");
            contractor_name = Console.ReadLine();
            Console.Write("Contractor email: ");
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
