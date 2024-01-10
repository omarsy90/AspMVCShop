using Marketing.DB;

namespace Marketing.Models
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly MarketingContext _marketingContext;


        public CustomerRepository(MarketingContext marketingContext)
        {
            _marketingContext = marketingContext;
        }

        public bool AddCustomer(string userID,string fullname, string country, string state, string street, int appartmentNumber, string telefon)
        {
            try
            {
                Customer customer = new Customer();

                customer.ID = userID;
                customer.CustomerFullName = fullname;
                customer.CustomerCountry = country;
                customer.CustomerState = state;
                customer.CustomerStreet = street;
                customer.AppartmentNumber = appartmentNumber;
                customer.TelefonNumber = telefon;

                _marketingContext.Customers.Add(customer);
                _marketingContext.SaveChanges();
                return true;
            }catch(Exception ex) {

                return false;
            }
           
        }

        public bool AddCustomer(Customer customer)
        {
            try
            {
                _marketingContext.Customers.Add(customer);
                _marketingContext.SaveChanges();
                return true;

            }catch(Exception e)
            {
                return false;
            }


        }

        public Customer GetCustomerById(string id)
        {
            return _marketingContext.Customers.First(customer=> customer.ID == id);  
        }

        
    }
}
