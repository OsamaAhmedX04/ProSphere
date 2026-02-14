using ProSphere.Domain.Constants;

namespace ProSphere.Helpers
{
    public static class OTPGenerator
    {
        public static string Generate()
        {
            string otp = "";
            otp += RandomDataGenerator.GenerateString(CharacterSets.numbers, 6);
            return otp;
        }
    }
}
