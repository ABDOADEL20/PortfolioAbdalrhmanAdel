using Microsoft.EntityFrameworkCore;
using PortfolioAbdo.BL.Interface;
using PortfolioAbdo.DAL.DataBase;
using PortfolioAbdo.DAL.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioAbdo.BL.Repository
{
    public class ApplicationUserRepository : IApplicationUser
    {
        private readonly PortfolioContext db;

        public ApplicationUserRepository(PortfolioContext db)
        {
            this.db = db;
        }
        public IEnumerable<ApplicationUser> Get()
        {
            IQueryable<ApplicationUser> data = GetInformations();
            return data;
        }

        private IQueryable<ApplicationUser> GetInformations()
        {
            return db.Users.Select(a => a);
        }

        public ApplicationUser GetByID(string id)
        {
            var data = db.Users.Where(a => a.Id == id).FirstOrDefault();
            return data;
        }

        public ApplicationUser Create(ApplicationUser obj)
        {
            db.Users.Add(obj);
            db.SaveChanges();

            return db.Users.OrderBy(a => a.Id).LastOrDefault();
        }

        //public void Delete(ApplicationUser obj)
        //{
        //    throw new NotImplementedException();
        //}

        public ApplicationUser Update(ApplicationUser obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();

            return db.Users.Where(a => a.Id == obj.Id).FirstOrDefault();
        }
    }
}
