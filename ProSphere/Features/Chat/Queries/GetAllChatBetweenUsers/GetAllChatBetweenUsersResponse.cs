namespace ProSphere.Features.Chat.Queries.GetAllChatBetweenUsers
{
    public class GetAllChatBetweenUsersResponse
    {
        public Guid MessageId { get; set; }
        public string SenderId { get; set; }
        public string? Message { get; set; }
        public string? ImageURL { get; set; }
        public DateTime SentAt { get; set; }
    }
}
