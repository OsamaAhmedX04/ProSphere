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
    }
}
