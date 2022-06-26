using PortfolioAbdo.DAL.Extend;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioAbdo.BL.Models
{
   public class TestimonialsVm
   {
        public int Id { get; set; }

        [Required(ErrorMessage = "This Field Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This Field Required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "This Field Required")]
        public string JobTitle { get; set; }

        [Required(ErrorMessage = "This Field Required")]
        public string CompanyName { get; set; }

        public string Photo { get; set; }

        [Required(ErrorMessage = "This Field Required")]
        public string Message { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }
        public ApplicationUser applicationUser { get; set; }

        public bool Is_Deleted { get; set; }
    }
}
