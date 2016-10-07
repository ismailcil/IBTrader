using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using IB.Core.Model;

namespace IB.Core.Tool
{
    public static class ToolMail
    {
        public static void SendMail(string from, List<string> to, string subject, string message, string userName, string password, string host, int port, List<string> cc = null, List<string> bcc = null, string mailDisplayName = null, List<Attachment> lsAttachment = null, MailPriority mailPriority = MailPriority.Normal)
        {
            var itemMail = new MdlMail
            {
                From = from,
                To = to,
                MailCcUser = cc,
                MailBccUser = bcc,
                Subject = subject,
                Body = message,
                NetworkCredential = new NetworkCredential(userName, password),
                IsBodyHtml = true,
                UserDefaultCredentials = true,
                Host = host,
                SmtpPort = port,
                SmtpDeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = false,
                Timeout = 10,
                DisplayName = mailDisplayName,
                LstAttachment = lsAttachment,
                MailPriority = mailPriority

            };

            ThreadStart th = delegate { SendMail(itemMail); };
            new Thread(th).Start();
        }
        private static void SendMail(MdlMail itemMail)
        {

            var msg = new MailMessage { From = new MailAddress(itemMail.From, itemMail.DisplayName) };
            foreach (var item in itemMail.To.Where(item => !string.IsNullOrEmpty(item)))
            {
                msg.To.Add(item);
            }
            if (itemMail.MailCcUser != null && itemMail.MailCcUser.Count > 0)
            {
                foreach (var item in itemMail.MailCcUser.Where(item => !string.IsNullOrEmpty(item)))
                {
                    msg.CC.Add(item);
                }
            }
            if (itemMail.MailBccUser != null && itemMail.MailBccUser.Count > 0)
            {
                foreach (var item in itemMail.MailBccUser.Where(item => !string.IsNullOrEmpty(item)))
                {
                    msg.Bcc.Add(item);
                }
            }
            if (itemMail.LstAttachment != null && itemMail.LstAttachment.Count > 0  )
            {
                foreach (var item in itemMail.LstAttachment)
                {
                    msg.Attachments.Add(item);
                }
            }
            msg.Subject = itemMail.Subject;
            msg.Body = itemMail.Body;
            msg.IsBodyHtml = itemMail.IsBodyHtml;
            msg.Priority = itemMail.MailPriority;


            var client = new SmtpClient
            {
                EnableSsl = itemMail.EnableSsl,
                UseDefaultCredentials = itemMail.UserDefaultCredentials,
                Credentials = itemMail.NetworkCredential,
                Host = itemMail.Host,
                Timeout = itemMail.Timeout,
                Port = itemMail.SmtpPort,
                DeliveryMethod = itemMail.SmtpDeliveryMethod
            };

            client.SendCompleted += (s, e) =>{client.Dispose();msg.Dispose();};
            client.SendAsync(msg, null);
        }
    }
}
