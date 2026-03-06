namespace ProSphere.Features.Chat.Queries.GetAllContactsForUser
{
    public class GetAllContactsForUserResponse
    {
        public string UserId { get; set; }
        public string UserFullName { get; set; }
        public string? UserImageProfileURL { get; set; }
        public string? LastMessage { get; set; }
        public DateTime LastMessageSentAt { get; set; }
    }
}
