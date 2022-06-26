using PortfolioAbdo.DAL.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioAbdo.BL.Interface
{
    public interface IApplicationUser
    {
        IEnumerable<ApplicationUser> Get();
        ApplicationUser GetByID(string id);
        ApplicationUser Create(ApplicationUser obj);
        ApplicationUser Update(ApplicationUser obj);
        //void Delete(ApplicationUser obj);
    }
}
