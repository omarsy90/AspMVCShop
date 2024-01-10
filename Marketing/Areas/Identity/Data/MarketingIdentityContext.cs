
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Marketing.Areas.Identity.Data;

public class MarketingIdentityContext : IdentityDbContext<MarketingUser>
{
    public MarketingIdentityContext(DbContextOptions<MarketingIdentityContext> options)
        : base(options)
    {
    }


   

    protected override void OnModelCreating(ModelBuilder builder)
    {
        IdentityRole defaultRole = new IdentityRole()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Default",
            NormalizedName = "Default",
            ConcurrencyStamp = Guid.NewGuid().ToString()
        };


        IdentityRole adminRole = new IdentityRole()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Admin",
            NormalizedName = "Admin",
            ConcurrencyStamp = Guid.NewGuid().ToString()
        };


        builder.Entity<IdentityRole>().HasData(defaultRole, adminRole);


       

        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
