using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using ProSphere.Domain.Constants.FileConstants;
using ProSphere.Domain.Entities;
using ProSphere.Extensions;
using ProSphere.ExternalServices.Interfaces.FileStorage;
using ProSphere.Hubs.Chat;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;
using ProSphere.Services.Notification;
using ProSphere.Shared.DTOs.Chat;

namespace ProSphere.Features.Chat.Commands.SendMessage
{
    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<SendMessageRequest> _validator;
        private readonly IHubContext<ChatHub> _hub;
        private readonly INotificationService _notificationService;
        private readonly IFileService _fileService;

        public SendMessageCommandHandler(IUnitOfWork unitOfWork, IValidator<SendMessageRequest> validator,
            IHubContext<ChatHub> hub, INotificationService notificationService, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _hub = hub;
            _notificationService = notificationService;
            _fileService = fileService;
        }

        public async Task<Result> Handle(SendMessageCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command.request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.ConvertErrorsToDictionary();
                return Result.ValidationFailure(errors);
            }




            // prepare message
            string? message = command.request.Message == null ? null : command.request.Message;
            string? imageURL = null;
            if (command.request.Image != null)
                imageURL = await _fileService.UploadAsync(command.request.Image);


            // get convesation
            var conversation = await _unitOfWork.Conversations
                .FirstOrDefaultAsync(c =>
                    (c.CreatorId == command.senderId && c.InvestorId == command.receiverId)
                    ||
                    (c.CreatorId == command.receiverId || c.InvestorId == command.senderId)
                    );

            // add new conversation for the first time
            if (conversation is null)
            {
                var isCreator = await _unitOfWork.Creators.IsExistAsync(command.senderId);
                var creatorId = isCreator ? command.senderId : command.receiverId;
                var investorId = isCreator ? command.receiverId : command.senderId;
                conversation = new Conversation
                {
                    Id = Guid.NewGuid(),
                    CreatorId = creatorId,
                    InvestorId = investorId,
                    LastMessage = message ?? "Image",
                    LastMessageSentAt = DateTime.UtcNow,
                };

                await _unitOfWork.Conversations.AddAsync(conversation);
            }
            else
            {
                conversation.LastMessage = message ?? "Image";
                conversation.LastMessageSentAt = DateTime.UtcNow;
            }



            var chatMessage = new Message
            {
                SenderId = command.senderId,
                ReceiverId = command.receiverId,
                Content = message,
                ImageURL = imageURL,
                ConversationId = conversation.Id
            };

            await _unitOfWork.Messages.AddAsync(chatMessage);


            // send via socket
            var messageDto = new MessageDto
            {
                ImageURL = SupabaseConstants.PrefixSupaURL + imageURL!,
                Message = message,
                SentAt = DateTime.UtcNow,
            };

            await _hub.Clients.Client(command.receiverId).SendAsync("ReceiveMessage", messageDto, cancellationToken);


            await _unitOfWork.CompleteAsync();
            return Result.Success("Message Sent Successfully");
        }
    }
}


//new NotificationDto
//{
//    Id = Guid.NewGuid(),
//    Status = "Info",
//    Type = "IncomingAccessRequest",
//    Title = "New Access Request",
//    Description = "An investor requested access to your project details.",
//    SentAt = DateTime.UtcNow,
//    SeenAt = null,
//    IsSeen = false
//};

//new NotificationDto
//{
//    Id = Guid.NewGuid(),
//    Status = "Success",
//    Type = "AccessRequestApproved",
//    Title = "Access Request Approved",
//    Description = "Your access request has been approved.",
//    SentAt = DateTime.UtcNow,
//    SeenAt = DateTime.UtcNow,
//    IsSeen = true
//};

//new NotificationDto
//{
//    Id = Guid.NewGuid(),
//    Status = "Failure",
//    Type = "AccessRequestRejected",
//    Title = "Access Request Rejected",
//    Description = "Unfortunately, your access request was rejected.",
//    SentAt = DateTime.UtcNow,
//    SeenAt = null,
//    IsSeen = false
//};

//new NotificationDto
//{
//    Id = Guid.NewGuid(),
//    Status = "Success",
//    Type = "ProjectModerationApproved",
//    Title = "Project Approved",
//    Description = "Your project passed moderation successfully.",
//    SentAt = DateTime.UtcNow,
//    SeenAt = null,
//    IsSeen = false
//};

//new NotificationDto
//{
//    Id = Guid.NewGuid(),
//    Status = "Warning",
//    Type = "ProjectModerationRejected",
//    Title = "Project Rejected",
//    Description = "Your project was rejected during moderation. Please check you mail to know rejection reason.",
//    SentAt = DateTime.UtcNow,
//    SeenAt = null,
//    IsSeen = false
//};

//new NotificationDto
//{
//    Id = Guid.NewGuid(),
//    Status = "Info",
//    Type = "ProjectVoted",
//    Title = "New Vote on Your Project",
//    Description = "Someone voted on your project.",
//    SentAt = DateTime.UtcNow,
//    SeenAt = null,
//    IsSeen = false
//};

//new NotificationDto
//{
//    Id = Guid.NewGuid(),
//    Status = "Success",
//    Type = "VerificationApproved",
//    Title = "Account Verified",
//    Description = "Your verification request has been approved.",
//    SentAt = DateTime.UtcNow,
//    SeenAt = DateTime.UtcNow,
//    IsSeen = true
//};

//new NotificationDto
//{
//    Id = Guid.NewGuid(),
//    Status = "Critical",
//    Type = "VerificationRejected",
//    Title = "Verification Rejected",
//    Description = "Your verification request was rejected. Please check your mail to know rejection reason and resubmit correct documents.",
//    SentAt = DateTime.UtcNow,
//    SeenAt = null,
//    IsSeen = false
//};