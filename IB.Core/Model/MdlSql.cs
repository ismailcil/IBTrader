using System.Collections.Generic;

namespace IB.Core.Model
{
    public class MdlSql
    {
        public string SqlQuery { get; set; }
        public string SqlDatabaseName { get; set; }
        public string SqlDatabaseIp { get; set; }
        public Dictionary<string,object> SqlParam { get; set; }

        public string ErrorMessage { get; set; }
        public string ErrorStackTrace { get; set; }
        public List<string> ErrorStackFrameMethodName { get; set; }
    }

    
}
