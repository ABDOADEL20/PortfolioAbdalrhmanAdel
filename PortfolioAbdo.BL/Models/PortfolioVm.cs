using Microsoft.AspNetCore.Http;
using PortfolioAbdo.DAL.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioAbdo.BL.Models
{
    public class PortfolioVm
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "* Project Name Required")]
        public string Project_Name { get; set; }

        public string Project_Photo_Name { get; set; }

        public IFormFile Project_Photo { get; set; }

        [Required(ErrorMessage = "* Project Description Required")]
        public string Project_Description { get; set; }

        [Required(ErrorMessage = "* Project Client Required")]
        public string Project_Client { get; set; }

        [Required(ErrorMessage = "* Project Date Required")]
        public DateTime Project_Date { get; set; }

        [Required(ErrorMessage = "* Project GitHub Required")]
        public string Project_GitHub { get; set; }

        [Required(ErrorMessage = "* Category Protoflio Required")]
        public int Category_ProtoflioId { get; set; }

        public Category_Portoflio Category_Portoflio { get; set; }

        public bool Is_Deleted { get; set; }
    }
}
