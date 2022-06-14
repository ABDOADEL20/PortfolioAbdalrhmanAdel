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
    public class HomeRepository : IHome
    {
        private readonly PortfolioContext db;

        public HomeRepository(PortfolioContext db)
        {
            this.db = db;
        }
        public IEnumerable<Homes> Get()
        {
            IQueryable<Homes> data = GetInformations();
            return data;
        }
        public Homes GetByID(int id)
        {
            var data = db.Home.Where(a => a.Id == id).FirstOrDefault();
            return data;
        }
        public Homes Create(Homes obj)
        {
            db.Home.Add(obj);
            db.SaveChanges();

            return db.Home.OrderBy(a => a.Id).LastOrDefault();
        }

        public void Delete(Homes obj)
        {
            db.Home.Remove(obj);
            db.SaveChanges();
        }

        public Homes Update(Homes obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();

            return db.Home.Where(a => a.Id == obj.Id).FirstOrDefault();
        }
        private IQueryable<Homes> GetInformations()
        {
            return db.Home.Select(a => a);
        }

       
    }
}
