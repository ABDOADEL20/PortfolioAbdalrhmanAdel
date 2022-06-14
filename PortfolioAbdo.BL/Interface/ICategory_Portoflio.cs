using PortfolioAbdo.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioAbdo.BL.Interface
{
   public interface ICategory_Portoflio
    {
        IEnumerable<Category_Portoflio> Get();
        Category_Portoflio GetByID(int id);
        Category_Portoflio Create(Category_Portoflio obj);
        Category_Portoflio Update(Category_Portoflio obj);
        void Delete(Category_Portoflio obj);
    }
}
