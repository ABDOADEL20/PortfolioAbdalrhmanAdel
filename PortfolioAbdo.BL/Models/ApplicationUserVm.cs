using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioAbdo.BL.Models
{
    public class ApplicationUserVm
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public IFormFile Photo { get; set; }
        public string PhotoUrl { get; set; }
        public string JobTitle { get; set; }
        public string CompanyName { get; set; }
    }
}
