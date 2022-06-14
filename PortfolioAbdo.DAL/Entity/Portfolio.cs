using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioAbdo.DAL.Entity
{
    [Table("Portfolio")]
    public class Portfolio
    {
        public int Id { get; set; }
        public string Project_Name { get; set; }
        public byte[] Project_Photo { get; set; }
        public string Project_Description { get; set; }
        public string Project_Service { get; set; }
        public string Project_Client { get; set; }
        public DateTime Project_Date { get; set; }
        public string Project_GitHub { get; set; }

        public int Category_ProtoflioId { get; set; }
        public Category_Portoflio Category_Portoflio { get; set; }

        public bool Is_Deleted { get; set; }
    }
}
