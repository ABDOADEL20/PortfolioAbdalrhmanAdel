using PortfolioAbdo.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioAbdo.BL.Interface
{
    public interface IExpCompaniesPhoto
    {
        IEnumerable<ExpCompaniesPhoto> Get();
        ExpCompaniesPhoto GetByID(int id);
        ExpCompaniesPhoto Create(ExpCompaniesPhoto obj);
        ExpCompaniesPhoto Update(ExpCompaniesPhoto obj);
        void Delete(ExpCompaniesPhoto obj);
    }
}
