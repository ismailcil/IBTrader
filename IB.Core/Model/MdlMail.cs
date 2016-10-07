using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace IB.Core.Model
{
    internal class MdlMail
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<string> To { get; set; }
        public string From { get; set; }
        public string DisplayName { get; set; }
        public List<string> MailCcUser { get; set; }
        public List<string> MailBccUser { get; set; }
        public bool EnableSsl { get; set; }
        public bool UserDefaultCredentials { get; set; }
        public NetworkCredential NetworkCredential { get; set; }
        public string Host { get; set; }
        public int Timeout { get; set; }
        public int SmtpPort { get; set; }
        public List<Attachment> LstAttachment { get; set; }
        public SmtpDeliveryMethod SmtpDeliveryMethod { get; set; }
        public bool IsBodyHtml  { get; set; }
        public MailPriority MailPriority  { get; set; }
    }


    public abstract class MailBodyReplaceClass
    {
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
    }

}
