using Microsoft.AspNetCore.Identity;
using PortfolioAbdo.DAL.Extend;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioAbdo.BL.Models
{
    public class MultipleModels
    {
        public IEnumerable<HomeVm> HomeVmAll { get; set; }

        public HomeVm HomeVm { get; set; }

        public ExpCompaniesPhotoVm ExpCompaniesPhotoVmModel { get; set; }

        public IEnumerable<ExpCompaniesPhotoVm> ExpCompaniesPhotoVm { get; set; }

        public ApplicationUserVm ApplicationUserVm { get; set; }

        public IEnumerable<ApplicationUserVm> ApplicationUserVmAll { get; set; }

        public TestimonialsVm TestimonialsVm { get; set; }

        public IEnumerable<TestimonialsVm> TestimonialsVmAll { get; set; }

        public IEnumerable<PortfolioVm> PortfolioVmAll { get; set; }

        public IEnumerable<Category_PortoflioVm> Category_PortoflioVmAll { get; set; }

    }
}
