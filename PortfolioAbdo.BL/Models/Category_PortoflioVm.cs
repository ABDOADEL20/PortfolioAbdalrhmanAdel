using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioAbdo.BL.Models
{
  public class Category_PortoflioVm
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "* Category Name Required")]
        public string Category_Name { get; set; }

        public bool Is_Deleted { get; set; }
    }
}
