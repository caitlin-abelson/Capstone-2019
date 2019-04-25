using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class EMailer
    {
        public static string EmailUsername { get; set; }
        public static string EmailPassword { get; set; }
        public static string EmailHost { get; set; }
        public static int EmailPort { get; set; }
        public static bool EmailSSL { get; set; }

        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; }

        static EMailer()
        {
            EmailHost = "smtp.gmail.com";
            EmailPort = 587;
            EmailSSL = true;
        }

        public void Send()
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Host = EmailHost;
            smtp.Port = EmailPort;
            smtp.EnableSsl = EmailSSL;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(EmailUsername, EmailPassword);

            using (var message = new MailMessage(EmailUsername, ToEmail))
            {
                message.Subject = Subject;
                message.Body = Body;
                message.IsBodyHtml = IsHtml;
                smtp.Send(message);
            }
        }
    }
} 