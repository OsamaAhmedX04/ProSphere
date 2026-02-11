using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProSphere.Features.Verification.Commands.AcceptFinancialInvestorVerification;
using ProSphere.Features.Verification.Commands.AcceptIdentityVerification;
using ProSphere.Features.Verification.Commands.AcceptProfessionalInvestorVerification;
using ProSphere.Features.Verification.Commands.RejectFinancialInvestorVerification;
using ProSphere.Features.Verification.Commands.RejectIdentityVerification;
using ProSphere.Features.Verification.Commands.RejectProfessionalInvestorVerification;
using ProSphere.Features.Verification.Commands.VerifyFinancialInvestor;
using ProSphere.Features.Verification.Commands.VerifyIdentity;
using ProSphere.Features.Verification.Commands.VerifyProfessionalInvestor;
using ProSphere.Features.Verification.Queries.GetFinancialInvestorVerificationById;
using ProSphere.Features.Verification.Queries.GetFinancialInvestorVerifications;
using ProSphere.Features.Verification.Queries.GetIdentityVerificationById;
using ProSphere.Features.Verification.Queries.GetIdentityVerifications;
using ProSphere.Features.Verification.Queries.GetProfessionalInvestorVerificationById;
using ProSphere.Features.Verification.Queries.GetProfessionalInvestorVerifications;

namespace ProSphere.Features.Verification.Endpoint
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerificationController : ControllerBase
    {
        private readonly ISender _sender;

        public VerificationController(ISender sender)
        {
            _sender = sender;
        }

        #region Get
        [HttpGet("identity/{identityDocumentId}")]
        public async Task<IActionResult> GetIdentityVerification(Guid identityDocumentId)
        {
            var query = new GetIdentityVerificationByIdQuery(identityDocumentId);
            var result = await _sender.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("financial/{financialDocumentId}")]
        public async Task<IActionResult> GetFinancialVerification(Guid financialDocumentId)
        {
            var query = new GetFinancialInvestorVerificationByIdQuery(financialDocumentId);
            var result = await _sender.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("professional/{professionalDocumentId}")]
        public async Task<IActionResult> GetProfessionalVerification(Guid professionalDocumentId)
        {
            var query = new GetProfessionalInvestorVerificationByIdQuery(professionalDocumentId);
            var result = await _sender.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("identities")]
        public async Task<IActionResult> GetIdentitiesVerification(int pageNumber, string? status = null)
        {
            var query = new GetIdentityVerificationQuery(pageNumber, status);
            var result = await _sender.Send(query);
            return StatusCode(result.StatusCode, result);
        }


        [HttpGet("financials")]
        public async Task<IActionResult> GetFinancialsVerification(int pageNumber, string? status = null)
        {
            var query = new GetFinancialInvestorVerificationsQuery(pageNumber, status);
            var result = await _sender.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("professionals")]
        public async Task<IActionResult> GetProfessionalVerification(int pageNumber, string? status = null)
        {
            var query = new GetProfessionalInvestorVerificationsQuery(pageNumber, status);
            var result = await _sender.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        #endregion


        #region Accept/Reject
        [HttpPut("identity/accept")]
        public async Task<IActionResult> AcceptIdentityVerification(string moderatorId, Guid identityDocumentId)
        {
            var command = new AcceptIdentityVerificationCommand(moderatorId, identityDocumentId);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("identity/reject")]
        public async Task<IActionResult> RejectIdentityVerification
            (string moderatorId, Guid identityDocumentId, RejectIdentityVerificationRequest request)
        {
            var command = new RejectIdentityVerificationCommand(moderatorId, identityDocumentId, request);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("financail/accept")]
        public async Task<IActionResult> AcceptFinancialVerification(string moderatorId, Guid financialDocumentId)
        {
            var command = new AcceptFinancialInvestorVerificationCommand(moderatorId, financialDocumentId);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("financial/reject")]
        public async Task<IActionResult> RejectFinancialVerification
            (string moderatorId, Guid financialDocumentId, RejectFinancialInvestorVerificationRequest request)
        {
            var command = new RejectFinancialInvestorVerificationCommand(moderatorId, financialDocumentId, request);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("professional/accept")]
        public async Task<IActionResult> AcceptProfessionalVerification(string moderatorId, Guid professionalDocumentId)
        {
            var command = new AcceptProfessionalInvestorVerificationCommand(moderatorId, professionalDocumentId);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("professional/reject")]
        public async Task<IActionResult> RejectProfessionalVerification
            (string moderatorId, Guid professionalDocumentId, RejectProfessionalInvestorVerificationRequest request)
        {
            var command = new RejectProfessionalInvestorVerificationCommand(moderatorId, professionalDocumentId, request);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }


        #endregion


        #region Verify

        [HttpPost("identity")]
        public async Task<IActionResult> VerifyIdentity(string userId, [FromForm] VerifyIdentityRequest request)
        {
            var command = new VerifyIdentityCommand(userId, request);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("financial")]
        public async Task<IActionResult> VerifyFinancail(string investorId, [FromForm] VerifyFinancialInvestorRequest request)
        {
            var command = new VerifyFinancialInvestorCommand(investorId, request);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("professional")]
        public async Task<IActionResult> VerifyProfessional(string investorId, [FromForm] VerifyProfessionalInvestorRequest request)
        {
            var command = new VerifyProfessionalInvestorCommand(investorId, request);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        #endregion

    }
}
