namespace ProSphere.Domain.Constants.CacheConstants
{
    public static class CacheKey
    {
        private const string Prefix = "ProSphere";


        public const string FinancailDocumentTypes = $"{Prefix}:FinancialDocumentTypes";
        public const string ProfessionalDocumentTypes = $"{Prefix}:ProfessionalDocumentTypes";

        public const string GetModeratorAvailableEmailsKey = $"{Prefix}:ModeratorAvailableEmails";


        public const string ReportReasonsKey = "ReportReasons";
        public const string ReportReasonsLengthKey = "ReportReasonsLength";


        public static string GetUserSocialMediaAccountsKey(string userName) => $"{Prefix}:UserSocialMediaAccounts:{userName}";
        public static string GetAdminAccountKey(string userName) => $"{Prefix}:AdminAccount:{userName}";
        public static string GetCreatorAccountKey(string userName) => $"{Prefix}:CreatorAccount:{userName}";
        public static string GetInvestorAccountKey(string userName) => $"{Prefix}:CreatorAccount:{userName}";
        public static string GetModeratorAccountKey(string userName) => $"{Prefix}:ModeratorAccount:{userName}";

    }
}
