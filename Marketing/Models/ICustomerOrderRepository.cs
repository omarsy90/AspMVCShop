namespace Marketing.Models
{
    public  interface  ICustomerOrderRepository
    {

        public Task<IEnumerable<ViewModelOrder>> GetOrderByCustomerId(string customerId);

    }
}
