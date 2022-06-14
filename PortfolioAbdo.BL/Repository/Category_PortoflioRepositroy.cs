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
    public class Category_PortoflioRepositroy : ICategory_Portoflio
    {
        private readonly PortfolioContext db;

        public Category_PortoflioRepositroy(PortfolioContext db)
        {
            this.db = db;
        }

        public IEnumerable<Category_Portoflio> Get()
        {
            IQueryable<Category_Portoflio> data = GetInformations();
            return data;
        }

        public Category_Portoflio GetByID(int id)
        {
            var data = db.Category_Protoflio.Where(a => a.Id == id).FirstOrDefault();
            return data;
        }
        public Category_Portoflio Create(Category_Portoflio obj)
        {
            db.Category_Protoflio.Add(obj);
            db.SaveChanges();

            return db.Category_Protoflio.OrderBy(a => a.Id).LastOrDefault();
        }

        public void Delete(Category_Portoflio obj)
        {
            db.Category_Protoflio.Remove(obj);
            db.SaveChanges();
        }

        public Category_Portoflio Update(Category_Portoflio obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();

            return db.Category_Protoflio.Where(a => a.Id == obj.Id).FirstOrDefault();
        }

        private IQueryable<Category_Portoflio> GetInformations()
        {
            return db.Category_Protoflio.Select(a => a);
        }
    }
}
