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
    public class ExpCompaniesPhotoRepository : IExpCompaniesPhoto
    {
        private readonly PortfolioContext db;

        public ExpCompaniesPhotoRepository(PortfolioContext db)
        {
            this.db = db;
        }
        public IEnumerable<ExpCompaniesPhoto> Get()
        {
            IQueryable<ExpCompaniesPhoto> data = GetInformations();
            return data;
        }

        public ExpCompaniesPhoto GetByID(int id)
        {
            var data = db.ExpCompaniesPhoto.Where(a => a.Id == id).FirstOrDefault();
            return data;
        }

        public ExpCompaniesPhoto Create(ExpCompaniesPhoto obj)
        {
            db.ExpCompaniesPhoto.Add(obj);
            db.SaveChanges();

            return db.ExpCompaniesPhoto.OrderBy(a => a.Id).LastOrDefault();
        }

        public void Delete(ExpCompaniesPhoto obj)
        {
            db.ExpCompaniesPhoto.Remove(obj);
            db.SaveChanges();
        }

        public ExpCompaniesPhoto Update(ExpCompaniesPhoto obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();

            return db.ExpCompaniesPhoto.Where(a => a.Id == obj.Id).FirstOrDefault();
        }

        private IQueryable<ExpCompaniesPhoto> GetInformations()
        {
            return db.ExpCompaniesPhoto.Select(a => a);
        }
    }
}
