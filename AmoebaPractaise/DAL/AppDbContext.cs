

using Amoeba.Models;
using AmoebaPractaise.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Amoeba.DAL
{
    public class AppDbContext:IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        public DbSet<Client> clients { get; set; }
        public DbSet<Profession> professions { get; set; }
    }
   
}
