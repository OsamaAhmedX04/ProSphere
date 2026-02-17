namespace ProSphere.Domain.Constants.SetsConstants
{
    public static class CharacterSets
    {
        public static readonly char[] numbers = "0123456789".ToCharArray();
        public static readonly char[] safeSpecialChars = "!@#$%^&*()-_=+".ToCharArray();
        public static readonly char[] lowerCaseLetters = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
        public static readonly char[] upperCaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
    }
}
