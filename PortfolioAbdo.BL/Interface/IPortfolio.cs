using PortfolioAbdo.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioAbdo.BL.Interface
{
   public interface IPortfolio
    {
        IEnumerable<Portfolio> Get();
        Portfolio GetByID(int id);
        Portfolio Create(Portfolio obj);
        Portfolio Update(Portfolio obj);
        void Delete(Portfolio obj);
    }
}
