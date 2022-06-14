using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioAbdo.DAL.Extend
{
   public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public bool IsAgree { get; set; }
        public string PhotoUrl { get; set; }
        public string JobTitle { get; set; }
        public string CompanyName { get; set; }

    }
}
