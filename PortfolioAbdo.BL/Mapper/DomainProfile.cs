using AutoMapper;
using PortfolioAbdo.BL.Models;
using PortfolioAbdo.DAL.Entity;
using PortfolioAbdo.DAL.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioAbdo.BL.Mapper
{
   public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Homes, HomeVm>();
            CreateMap<HomeVm, Homes>();

            CreateMap<Category_Portoflio, Category_PortoflioVm>();
            CreateMap<Category_PortoflioVm, Category_Portoflio>();

            CreateMap<Portfolio, PortfolioVm>();
            CreateMap<PortfolioVm, Portfolio>();

            CreateMap<ExpCompaniesPhoto, ExpCompaniesPhotoVm>();
            CreateMap<ExpCompaniesPhotoVm, ExpCompaniesPhoto>();

            CreateMap<ApplicationUser, ApplicationUserVm>();
            CreateMap<ApplicationUserVm, ApplicationUser>();

            CreateMap<Testimonials, TestimonialsVm>();
            CreateMap<TestimonialsVm, Testimonials>();
        }
    }
}
