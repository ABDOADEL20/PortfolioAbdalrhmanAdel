using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioAbdo.DAL.Entity
{
    [Table("Home")]
    public class Homes
    {
        public int Id { get; set; }
        public string Banner_Name { get; set; }
        public string Banner_Job_Title { get; set; }
        public byte[] Cv { get; set; }
        public string Introduce_your_self { get; set; }
        public int Years_Of_Experience { get; set; }
        public string Link_facebook { get; set; }
        public string Link_twitter { get; set; }
        public string Link_linkedin { get; set; }

    }
}
