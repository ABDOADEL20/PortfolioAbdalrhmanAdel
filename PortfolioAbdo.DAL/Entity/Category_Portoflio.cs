using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioAbdo.DAL.Entity
{
    [Table("Category_Portoflio")]
    public class Category_Portoflio
    {
        public int Id { get; set; }
        public string Category_Name { get; set; }
        public bool Is_Deleted { get; set; }

    }
}
