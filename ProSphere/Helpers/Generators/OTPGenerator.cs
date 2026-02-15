using ProSphere.Domain.Constants.SetsConstants;

namespace ProSphere.Helpers.Generators
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
