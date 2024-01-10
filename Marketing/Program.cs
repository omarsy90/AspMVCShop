using Marketing.DB;
using Marketing.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Marketing.Areas.Identity.Data;


using Stripe;
using Microsoft.AspNetCore.Authorization;
using Azure;
using System.Net;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Marketing
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
                        var connectionString = builder.Configuration.GetConnectionString("MarketingIdentityContextConnection") ?? throw new InvalidOperationException("Connection string 'MarketingIdentityContextConnection' not found.");

               builder.Services.AddDbContext<MarketingIdentityContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<MarketingUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
             .AddEntityFrameworkStores<MarketingIdentityContext>();







            builder.Services.AddSession();
         


            // Add services to the container.
            builder.Services.AddControllersWithViews();
          
            //

            builder.Services.AddDbContext<MarketingContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("myconn")));
                    

            builder.Services.AddTransient<ICategoriesRepository,CategoriesRepository>();
            builder.Services.AddTransient<IProducktsRepository,ProductsRepository>();

            builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
            
            builder.Services.AddTransient<UserManager<MarketingUser>, UserManager<MarketingUser>>();

            builder.Services.AddTransient<IShopingCardRepository, ShopingCardRepository>();

            builder.Services.AddTransient<IOrderRepository, OrderRepository>();
            builder.Services.AddTransient<IOrderDetailRepository, OrderDetailRepository>();

            builder.Services.AddTransient<ICustomerOrderRepository, OrderRepository>();


            builder.Services.AddTransient<IEmailSender, EmailSender>();
            builder.Services.AddTransient<SignInManager<MarketingUser>>();


            builder.Services.AddMvc();


            // Stripe  setting 

          var ApiSecretKey=  builder.Configuration.GetValue<string>("StripeSekretKey");
            StripeConfiguration.SetApiKey(ApiSecretKey);



            //


    //        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    //.AddCookie(options =>
    //{
    //    options.LoginPath = "/Account/Login";
    //    options.LogoutPath = "/Account/LogOut";
       
    //});

            




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }







           




            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
        
             app.UseAuthentication();;
            app.UseAuthorization();


           

   



            app.MapRazorPages();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}