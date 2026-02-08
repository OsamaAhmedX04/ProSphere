namespace ProSphere.Domain.Constants
{
    public class ProfessionalType
    {
        public static string CommercialRegister = "Commercial Register";
        public static string TaxCard = "Tax Card";
        public static string Membership = "Membership";
        public static string Letter = "Letter";
        public static string Other = "Other";

        public static List<string> Types = new List<string>() { CommercialRegister, TaxCard, Membership, Letter, Other };


    }
}
