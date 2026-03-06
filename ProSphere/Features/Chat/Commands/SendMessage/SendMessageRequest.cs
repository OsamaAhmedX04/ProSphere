namespace ProSphere.Features.Chat.Commands.SendMessage
{
    public class SendMessageRequest
    {
        public string? Message { get; set; }
        public IFormFile? Image { get; set; }
    }
}
