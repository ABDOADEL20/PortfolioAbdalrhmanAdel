using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PortfolioAbdo.DAL.Entity;
using PortfolioAbdo.DAL.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioAbdo.DAL.DataBase
{
  public class PortfolioContext : IdentityDbContext<ApplicationUser>
    {
        public PortfolioContext(DbContextOptions<PortfolioContext> opt) : base(opt)
        {

        }

        public DbSet<Homes> Home { get; set; }
        public DbSet<Category_Portoflio> Category_Protoflio { get; set; }
        public DbSet<Portfolio> Portfolio { get; set; }
        public DbSet<Testimonials> Testimonials { get; set; }
        public DbSet<ExpCompaniesPhoto> ExpCompaniesPhoto { get; set; }


    }
}
