using PortfolioAbdo.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioAbdo.BL.Interface
{
   public interface ITestimonials
    {
        IEnumerable<Testimonials> Get();
        Testimonials GetByID(int id);
        IEnumerable<Testimonials> GetByIDUser(string id);
        Testimonials Create(Testimonials obj);
        Testimonials Update(Testimonials obj);
        void Delete(Testimonials obj);
    }
}
