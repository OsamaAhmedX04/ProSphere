using MediatR.NotificationPublishers;

namespace ProSphere.Features.Admin.Commands.CreateAdmin
{
    public class CreateAdminRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
