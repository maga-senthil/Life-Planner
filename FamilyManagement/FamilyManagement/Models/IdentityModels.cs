using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FamilyManagement.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("FamilyManagement", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<Category> category { get; set; }
        public DbSet<ItemCategory> itemCategory { get; set; }
        public DbSet<MonthlyBudget> monthlyBudget { get; set; }
        public DbSet<DailyExpense> dailyExpense { get; set; }
        public DbSet<Family> family { get; set; }
        public DbSet<Chore> chore { get; set; }
        public DbSet<DailyChore> dailyChore { get; set; }
        public DbSet<Events> events { get; set; }
        public DbSet<Maintanance> maintanance { get; set; }
        public DbSet<EmergencyContact> emergencyContact { get; set; }
        public DbSet<Person> person { get; set; }

    }
}