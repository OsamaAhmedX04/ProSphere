namespace ProSphere.Features.Authentication.Commands.ChangeInActiveRolePassword
{
    public class ChangeInActiveRolePasswordRequest
    {
        public string UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public bool RememberMe { get; set; } = false;
    }
}
