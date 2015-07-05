using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace MVC
{
    public class Notifications
    {
        public static void Send(string to, string cc, string bcc, string subject, string body, bool isHtml)
        {
            try
            {
                MailMessage oMsg = new MailMessage();
                oMsg.From = new MailAddress("no-reply@schoolgap.com", "SchoolGap Jobs");
                oMsg.To.Add(to);
                if (cc != string.Empty) oMsg.CC.Add(cc);
                if (bcc != string.Empty) oMsg.Bcc.Add(bcc);
                oMsg.Subject = subject;
                oMsg.IsBodyHtml = isHtml;
                oMsg.Body = body;
                
                SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["host"].ToString(), Convert.ToInt16(ConfigurationManager.AppSettings["port"].ToString()));
                smtp.Send(oMsg);
                oMsg = null;
                
                
            }
            catch (Exception ex)
            {
                
            }


        }

        
    }


}