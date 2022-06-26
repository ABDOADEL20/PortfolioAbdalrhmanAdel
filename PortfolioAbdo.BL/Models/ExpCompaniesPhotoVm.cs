using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioAbdo.BL.Models
{
   public class ExpCompaniesPhotoVm
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "* Image file Required")]
        public IFormFile Image { get; set; }

        public string ImageName { get; set; }
    }
}
