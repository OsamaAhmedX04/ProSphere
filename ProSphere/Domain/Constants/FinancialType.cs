namespace ProSphere.Domain.Constants
{
    public static class FinancialType
    {
        public static string BankStatement = "Bank Statement";
        public static string Wallet = "Wallet";
        public static string FinancialLetter = "Financial Letter";
        public static string Other = "Other";
        public static List<string> Types = new List<string>() { BankStatement,Wallet,FinancialLetter,Other};
    }
}
