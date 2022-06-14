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
    public class PortfolioRepository : IPortfolio
    {
        private readonly PortfolioContext db;

        public PortfolioRepository(PortfolioContext db)
        {
            this.db = db;
        }

        public IEnumerable<Portfolio> Get()
        {
            IQueryable<Portfolio> data = GetInformations();
            return data;
        }

        public Portfolio GetByID(int id)
        {
            var data = db.Portfolio.Where(a => a.Id == id).FirstOrDefault();
            return data;
        }

        public Portfolio Create(Portfolio obj)
        {
            db.Portfolio.Add(obj);
            db.SaveChanges();

            return db.Portfolio.OrderBy(a => a.Id).LastOrDefault();
        }

        public void Delete(Portfolio obj)
        {
            db.Portfolio.Remove(obj);
            db.SaveChanges();
        }

        public Portfolio Update(Portfolio obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();

            return db.Portfolio.Where(a => a.Id == obj.Id).FirstOrDefault();
        }

        private IQueryable<Portfolio> GetInformations()
        {
            return db.Portfolio.Select(a => a);
        }
    }
}
