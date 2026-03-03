using Microsoft.AspNetCore.Identity;
using ProSphere.Domain.Constants.FileConstants;
using ProSphere.Domain.Constants.RoleConstants;
using ProSphere.Domain.Entities;
using ProSphere.Domain.Enums;
using ProSphere.ExternalServices.Interfaces.FileStorage;
using ProSphere.RepositoryManager.Interfaces;

namespace ProSphere.Jobs.Account.DeleteAccount
{
    public class DeleteAccountJob : IDeleteAccountJob
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;

        public DeleteAccountJob(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IFileService fileService)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }

        public async Task DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return;

            var identityHistory = await _unitOfWork.IdentityVerificationHistories.FirstOrDefaultAsync(h => h.UserEmail == user.Email);
            var financialHistory = await _unitOfWork.FinancialVerificationHistories.FirstOrDefaultAsync(h => h.InvestorEmail == user.Email);
            var professionalHistory = await _unitOfWork.ProfessionalVerificationHistories.FirstOrDefaultAsync(h => h.InvestorEmail == user.Email);


            if (identityHistory != null)
            {
                _unitOfWork.IdentityVerificationHistories.Delete(identityHistory.Id);
                await _fileService.DeleteAsync(identityHistory.IdBackImageURL);
                await _fileService.DeleteAsync(identityHistory.IdFrontImageURL);
                await _fileService.DeleteAsync(identityHistory.SelfieWithIdURL);

            }
            if (financialHistory != null)
            {
                _unitOfWork.FinancialVerificationHistories.Delete(financialHistory.Id);
                await _fileService.DeleteAsync(financialHistory.DocumentURL);

            }
            if (professionalHistory != null)
            {
                _unitOfWork.ProfessionalVerificationHistories.Delete(professionalHistory.Id);
                await _fileService.DeleteAsync(professionalHistory.DocumentURL);
            }

            await _unitOfWork.UserAccountHistories.BulkDeleteAsync(h => h.Email == user.Email);

            await _unitOfWork.ChatMessagesHistories.BulkDeleteAsync(x => x.SenderEmail == user.Email || x.ReceiverEmail == user.Email);

            await _unitOfWork.CompleteAsync();

            await _userManager.DeleteAsync(user);


        }

        public async Task DeleteUselessUserDataAsync(ApplicationUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles.Contains(Role.Investor) || userRoles.Contains(Role.Creator))
            {
                await _unitOfWork.IdentityVerifications.BulkDeleteAsync(
                    x => x.UserId == user.Id && (x.status == Status.Rejected || x.status == Status.Pending));

                await _unitOfWork.ReportedProjects.BulkDeleteAsync(r => r.ReporterId == user.Id);
                await _unitOfWork.ReportedUsers.BulkDeleteAsync(r => r.ReporterId == user.Id);
            }

            if (userRoles.Contains(Role.Investor))
            {
                await _unitOfWork.FinancialVerifications
                    .BulkDeleteAsync(x => x.InvestorId == user.Id && (x.status == Status.Rejected || x.status == Status.Pending));

                await _unitOfWork.ProfessionalVerifications
                    .BulkDeleteAsync(x => x.InvestorId == user.Id && (x.status == Status.Rejected || x.status == Status.Pending));


            }

            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteBusinessUserDataAsync(ApplicationUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var role = userRoles.First();

            if (role == Role.Investor)
            {
                await _unitOfWork.ProjectsAccessRequests.BulkDeleteAsync(i => i.InvestorId == user.Id);
            }
            else
            {
                await _unitOfWork.Projects.BulkDeleteAsync(i => i.CreatorId == user.Id);
            }
        }

        public async Task MoveUserChatsAsync(ApplicationUser user)
        {
            var messages = await _unitOfWork.ChatMessages
                .GetAllAsyncEnhanced(x => x.SenderId == user.Id || x.ReceiverId == user.Id);

            var chathistories = messages.Select(x => new ChatMessageHistory
            {
                Message = x.Message,
                SentAt = x.SentAt,
                SenderEmail = x.Sender!.Email!,
                ReceiverEmail = x.Receiver!.Email!
            }).ToList();

            await _unitOfWork.ChatMessagesHistories.AddRangeAsync(chathistories);
            await _unitOfWork.ChatMessages.BulkDeleteAsync(x => x.SenderId == user.Id || x.ReceiverId == user.Id);

            await _unitOfWork.CompleteAsync();
        }

        public async Task MovePrivacyUserDataAsync(ApplicationUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles.Contains(Role.Investor) || userRoles.Contains(Role.Creator))
            {
                var identityVerification = await _unitOfWork.IdentityVerifications
                    .FirstOrDefaultAsync(x => x.UserId == user.Id && x.status == Status.Approved);
                if (identityVerification != null)
                {
                    var history = new IdentityVerificationHistory
                    {
                        IdBackImageURL = identityVerification.IdBackImageURL,
                        IdFrontImageURL = identityVerification.IdFrontImageURL,
                        SelfieWithIdURL = identityVerification.SelfieWithIdURL,
                        UserEmail = user.Email!
                    };
                    await _unitOfWork.IdentityVerificationHistories.AddAsync(history);
                    _unitOfWork.IdentityVerifications.Delete(identityVerification.Id);
                }
            }

            if (userRoles.Contains(Role.Investor))
            {
                var financialVerification = await _unitOfWork.FinancialVerifications
                    .FirstOrDefaultAsync(x => x.InvestorId == user.Id && x.status == Status.Approved);
                if (financialVerification != null)
                {
                    var history = new FinancialVerificationHistory
                    {
                        DocumentTypeId = financialVerification.DocumentTypeId,
                        DocumentURL = financialVerification.DocumentURL,
                        InvestorEmail = user.Email!
                    };
                    await _unitOfWork.FinancialVerificationHistories.AddAsync(history);
                    _unitOfWork.FinancialVerifications.Delete(financialVerification.Id);
                }

                var professionalVerification = await _unitOfWork.ProfessionalVerifications
                    .FirstOrDefaultAsync(x => x.InvestorId == user.Id && x.status == Status.Approved);
                if (professionalVerification != null)
                {
                    var history = new ProfessionalVerificationHistory
                    {
                        DocumentTypeId = professionalVerification.DocumentTypeId,
                        DocumentURL = professionalVerification.DocumentURL,
                        InvestorEmail = user.Email!
                    };
                    await _unitOfWork.ProfessionalVerificationHistories.AddAsync(history);
                    _unitOfWork.ProfessionalVerifications.Delete(professionalVerification.Id);
                }
            }

            await _unitOfWork.UserAccountHistories.AddAsync(new UserAccountHistory { Email = user.Email! });

            if (userRoles.Contains(Role.Creator))
            {
                var creator = await _unitOfWork.Creators.FirstOrDefaultAsync(x => x.Id == user.Id);
                if (!string.IsNullOrWhiteSpace(creator?.ImageProfileURL))
                    await _fileService.DeleteAsync(creator.ImageProfileURL);

                if (!string.IsNullOrWhiteSpace(creator?.CVURL))
                    await _fileService.DeleteAsync(creator.CVURL);

                _unitOfWork.Creators.Delete(user.Id);
            }
            if (userRoles.Contains(Role.Investor))
            {
                var investor = await _unitOfWork.Investors.FirstOrDefaultAsync(x => x.Id == user.Id);
                if (!string.IsNullOrWhiteSpace(investor?.ImageProfileURL))
                    await _fileService.DeleteAsync(investor.ImageProfileURL);

                _unitOfWork.Investors.Delete(user.Id);

            }
            await _userManager.DeleteAsync(user);

            await _unitOfWork.CompleteAsync();
        }


    }

}
