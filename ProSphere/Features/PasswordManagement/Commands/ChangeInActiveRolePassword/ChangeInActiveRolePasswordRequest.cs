namespace ProSphere.Features.PasswordManagement.Commands.ChangeInActiveRolePassword
{
    public class ChangeInActiveRolePasswordRequest
    {
        public string Email { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public bool RememberMe { get; set; } = false;
    }
}
