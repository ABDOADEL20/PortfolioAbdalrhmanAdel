using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioAbdo.BL.Models
{
   public class HomeVm 
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "* Banner Name Required")]
        public string Banner_Name { get; set; }

        [Required(ErrorMessage = "* Banner Job Title  Required")]
        public string Banner_Job_Title { get; set; }

        [Required(ErrorMessage = "* Cv file Required")]
        public byte[] Cv { get; set; }

        [Required(ErrorMessage = "* Introduce your self Required")]
        public string Introduce_your_self { get; set; }

        [Required(ErrorMessage = "* Years Of Experience Required")]
        public int Years_Of_Experience { get; set; }

        [Required(ErrorMessage = "* Link facebook Required")]
        public string Link_facebook { get; set; }

        [Required(ErrorMessage = "* Link twitter Required")]
        public string Link_twitter { get; set; }

        [Required(ErrorMessage = "* Link Linkedin Required")]
        public string Link_linkedin { get; set; }
    }
}
