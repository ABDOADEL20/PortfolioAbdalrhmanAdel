using PortfolioAbdo.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioAbdo.BL.Helper
{
    public class SendMailHelper
    {
        public static string MailSender(MailVm mail)
        {
            try
            {
                using (var smtp = new SmtpClient("smtp.office365.com", 587))
                {
                    smtp.EnableSsl = true;
                    smtp.Credentials = new NetworkCredential("abdalrhman.adel.portfolio@outlook.com", "Abdo @ 941998#");
                    smtp.Send("abdalrhman.adel.portfolio@outlook.com", mail.To, mail.Title, mail.Message);
                }
                return "Mail Sent Successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
