using Microsoft.EntityFrameworkCore;
using Marketing.Models;
using Microsoft.Extensions.Options;

namespace Marketing.DB
{
    public class MarketingContext :DbContext
    {
        public MarketingContext(DbContextOptions<MarketingContext> options) : base(options)
        {
           
        }


        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }

       
        public DbSet<Product> Products { get; set; }
       
        public DbSet<ShopingCart> ShopingCarts { get; set; }  
        
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrdersDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Category>().HasData(CreateRundomCategories());
            modelBuilder.Entity<Product>().HasData(CreateRundomProduct());
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }




        private  IEnumerable<Category> CreateRundomCategories()
        {
            Category ct1 = new Category()
            {
                ID = 1,
                CategoryName = "Men",

            };

            Category ct2 = new Category()
            {
                ID=2,
                CategoryName = "Women",
            };
            Category ct3 = new Category()
            {
                ID =3,
                CategoryName = "Children"
            };

            return new List<Category>() { ct1, ct2, ct3 };


        }



        private IEnumerable<Product> CreateRundomProduct()
        {
            



            Product pro1 = new Product()
            {
                
                ProductName = "t-shert",
                ProductDescription = "some thing",
                ProductPrise = 30.99M,
                ImgUrl = "images/products/1.jpg",
                ProductCategoryID = 1

            };


            Product pro2 = new Product()
            {
                ProductName = "jaket",
                ProductDescription = "some thing 2",
                ProductPrise = 98.55M,
                ImgUrl = "images/products/2.jpg",
                ProductCategoryID = 1
            };

            Product pro3 = new Product()
            {
                ProductName = "hoddy",
                ProductDescription = "some thing 3",
                ProductPrise = 98.55M,
                ImgUrl = "images/products/3.jpg",
                ProductCategoryID = 1
            };


            Product pro4 = new Product()
            {
                ProductName = "t-shert",
                ProductDescription = "some thing 4",
                ProductPrise = 30.99M,
                ImgUrl = "images/products/4.jpg",
                ProductCategoryID = 2
            };


            Product pro5 = new Product()
            {
                ProductName = "jaket",
                ProductDescription = "some thing 5",
                ProductPrise = 98.55M,
                ImgUrl = "images/products/5.jpg",
                ProductCategoryID = 2
            };

            Product pro6 = new Product()
            {
                ProductName = "hoddy",
                ProductDescription = "some thing 6",
                ProductPrise = 98.55M,
                ImgUrl = "images/products/6.jpg",
                ProductCategoryID = 2
            };




            Product pro7 = new Product()
            {
                ProductName = "t-shert",
                ProductDescription = "some thing 7",
                ProductPrise = 30.99M,
                ImgUrl = "images/products/7.jpg",
                ProductCategoryID = 3
            };


            Product pro8 = new Product()
            {
                ProductName = "jaket",
                ProductDescription = "some thing 8",
                ProductPrise = 98.55M,
                ImgUrl = "images/products/8.jpg",
                ProductCategoryID = 3
            };

            Product pro9 = new Product()
            {
                ProductName = "hoddy",
                ProductDescription = "some thing 9",
                ProductPrise = 98.55M,
                ImgUrl = "images/products/9.jpg",
                ProductCategoryID = 3
            };


            var produtcs = new List<Product>()
            {
                pro1,pro2,pro3,pro4,pro5,pro6,pro7,pro8,pro9

            };

            int i = 1;

            foreach(Product product in produtcs)
            {
                product.ID = i;
                i++;
            }


            return produtcs;
         

        }








    }
}
