namespace Marketing.Models
{
    public class Customer
    {


        //customer id is the same userId in ASP.NetUsers Table
        public string ID { get; set; }
        public string CustomerFullName { get; set; }
        
        public string CustomerCountry { get; set; }
        public string CustomerState { get; set; }
        public string CustomerStreet { get; set; }
        public int  AppartmentNumber { get; set; }  
        public string TelefonNumber { get; set; }

        public string Email { get; set; }

    }
}
