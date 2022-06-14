using PortfolioAbdo.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioAbdo.BL.Interface
{
   public interface IHome
    {
        IEnumerable<Homes> Get();
        Homes GetByID (int id);
        Homes Create(Homes obj);
        Homes Update(Homes obj);
        void Delete(Homes obj);
    }
}
