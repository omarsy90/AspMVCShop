namespace Marketing.Models
{
    public interface ICustomerRepository
    {
        public bool AddCustomer(string userID, string fullname, string country, string state, string street, int appartmentNumber,string telefon);

        public bool AddCustomer(Customer customer);

        public Customer GetCustomerById(string id);
    }
}
