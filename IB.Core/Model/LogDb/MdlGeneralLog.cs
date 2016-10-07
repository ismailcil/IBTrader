using System;

namespace IB.Core.Model.LogDb
{
    public class MdlGeneralLog
    {
        public int GeneralLogId { get; set; }
        public string GeneralLogFunctionName { get; set; }
        public string GeneralLogUserIp { get; set; }
        public string GeneralLogRequest { get; set; }
        public DateTime GeneralLogCreateDate { get; set; }
        public int? GeneralLogUserId { get; set; }
    }
}
