using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioAbdo.BL.Models
{
    public class MultipleModels
    {
        public HomeVm HomeVm { get; set; }
        public IEnumerable<ExpCompaniesPhotoVm> ExpCompaniesPhotoVm { get; set; }
    }
}
