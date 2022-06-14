using PortfolioAbdo.DAL.Extend;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioAbdo.DAL.Entity
{
    [Table("Testimonials")]
    public class Testimonials
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string JobTitle { get; set; }
        public string CompanyName { get; set; }
        public byte[] Photo { get; set; }
        public string Message { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser applicationUser { get; set; }

        public bool Is_Deleted { get; set; }

    }
}
