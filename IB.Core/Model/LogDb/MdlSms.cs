using System;

namespace IB.Core.Model.LogDb
{
    public class MdlSms
    {
        public int SmsId { get; set; }
        public DateTime SmsSendDate { get; set; }
        public DateTime? SmsDeliveryDate { get; set; }
        public int? SmsUserId { get; set; }
        public int SmsCentAppId { get; set; }
        public string SmsPhoneNumber { get; set; }
        public string SmsMessage { get; set; }
        public int SmsStatus { get; set; }
        public string SmsApiId { get; set; }
        public string SmsGuid { get; set; }
        public string SmsDescription { get; set; }
        public string SmsAccountNumber { get; set; }
    }
}
