using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioAbdo.DAL.Entity
{
    [Table("ExpCompaniesPhoto")]
    public class ExpCompaniesPhoto
    {
        public int Id { get; set; }

        public byte[] Image { get; set; }
    }
}
