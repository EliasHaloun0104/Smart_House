namespace Client
{
    public class Customer
    {    
        private long ssn;
        private string name;

        public Customer()
        {
        }

        public Customer(long ssn, string name)
        {
            this.ssn = ssn;
            this.name = name;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public long Ssn
        {
            get => ssn;
            set => ssn = value;
        }

        public override string ToString()
        {
            return "Name: " + name + "\n" + "SSN: " + ssn;
        }
    }
}