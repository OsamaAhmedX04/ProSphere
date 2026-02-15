namespace ProSphere.Domain.Constants.CacheConstants
{
    public static class CacheKey
    {
        private const string Prefix = "ProSphere";


        public const string FinancailDocumentTypes = $"{Prefix}:FinancialDocumentTypes";
        public const string ProfessionalDocumentTypes = $"{Prefix}:ProfessionalDocumentTypes";

        public static string GetUserSocialMediaAccountsKey(string userId) => $"{Prefix}:UserSocialMediaAccounts:{userId}";
        public static string GetAdminAccountKey(string userId) => $"{Prefix}:AdminAccount:{userId}";
        public static string GetCreatorAccountKey(string userId) => $"{Prefix}:CreatorAccount:{userId}";
        public static string GetInvestorAccountKey(string userId) => $"{Prefix}:CreatorAccount:{userId}";
        public static string GetModeratorAccountKey(string userId) => $"{Prefix}:ModeratorAccount:{userId}";


    }
}
