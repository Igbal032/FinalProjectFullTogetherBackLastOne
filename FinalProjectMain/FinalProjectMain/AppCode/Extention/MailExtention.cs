using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace FinalProjectMain
{
    public static class MailExtention
    {
        static public bool SendMail(string subject, string body, string toMails)
        {
            if (string.IsNullOrEmpty(subject))
                throw new ArgumentNullException("subject");
            if (string.IsNullOrEmpty(body))
                throw new ArgumentNullException("body");
            if (string.IsNullOrEmpty(toMails))
                throw new ArgumentNullException("toMails");

            MailMessage mail = new MailMessage(new MailAddress("fullstackstaff@mail.ru", "Qədimi Qablar Mağazası"), new MailAddress(toMails))
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            SmtpClient client = new SmtpClient();
            //{
            //    Host = "smtp.mail.ru",
            //    Credentials = new NetworkCredential("fullstackstaff", "!d@v#l0pmentgroup20!9"),
            //    Port = 25,
            //    DeliveryMethod = SmtpDeliveryMethod.Network,
            //    EnableSsl = true
            //};

            int retry = 3;
            do
            {
                try
                {
                    client.Send(mail);
                    return true;
                }
                catch (Exception ex) { }

                retry--;
            } while (retry > 0);

            return false;
        }
    }
}