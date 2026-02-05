using System.Net;

namespace ProSphere.Helpers
{
    public static class LinkGenerator
    {
        public static string GenerateEmailConfirmationLink(string userId, string token)
        {
            var encodedToken = WebUtility.UrlEncode(token);

            var confirmationLink = $"https://app.prosphere.com/confirm-email?userId={userId}&token={encodedToken}";

            return confirmationLink;
        }

        public static string GenerateResetPasswordLink(string userId, string token)
        {
            var encodedToken = WebUtility.UrlEncode(token);

            var resettingLink = $"https://app.prosphere.com/reset-password?userId={userId}&token={encodedToken}";

            return resettingLink;
        }
    }
}
