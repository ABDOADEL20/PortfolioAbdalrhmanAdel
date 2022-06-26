using Microsoft.EntityFrameworkCore;
using PortfolioAbdo.BL.Interface;
using PortfolioAbdo.DAL.DataBase;
using PortfolioAbdo.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioAbdo.BL.Repository
{
    public class TestimonialsRepository : ITestimonials
    {
        private readonly PortfolioContext db;

        public TestimonialsRepository(PortfolioContext db)
        {
            this.db = db;
        }
        public IEnumerable<Testimonials> Get()
        {
            IQueryable<Testimonials> data = GetInformations();
            return data;
        }
        public IEnumerable<Testimonials> GetByIDUser(string id)
        {
            IQueryable<Testimonials> data = db.Testimonials.Where(a => a.ApplicationUserId == id);
            return data;
        }
        private IQueryable<Testimonials> GetInformations()
        {
            return db.Testimonials.Select(a => a);
        }

        public Testimonials GetByID(int id)
        {
            var data = db.Testimonials.Where(a => a.Id == id).FirstOrDefault();
            return data;
        }

        public Testimonials Create(Testimonials obj)
        {
            db.Testimonials.Add(obj);
            db.SaveChanges();

            return db.Testimonials.OrderBy(a => a.Id).LastOrDefault();
        }

        public void Delete(Testimonials obj)
        {
            db.Testimonials.Remove(obj);
            db.SaveChanges();
        }

        public Testimonials Update(Testimonials obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();

            return db.Testimonials.Where(a => a.Id == obj.Id).FirstOrDefault();
        }

       
    }
}
