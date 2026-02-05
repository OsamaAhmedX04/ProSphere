using ProSphere.Domain.Constants;

namespace ProSphere.Helpers
{
    public static class OTPGenerator
    {
        public static string GenerateRandomOTP()
        {
            string otp = "";
            otp += RandomDataGenerator.GetRandomString(CharacterSets.numbers, 6);
            return otp;
        }
    }
}
