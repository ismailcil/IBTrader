using IB.Core.Properties;

namespace IB.Core.Tool
{
    public static class ToolGeneralParameter
    {
        public static string MailServerUserName = Settings.Default.MailServerUserName;
        public static string MailServerPassword = Settings.Default.MailServerPassword;
        public static readonly string ConnectionString = Settings.Default.ConnectionString;
    }
}
