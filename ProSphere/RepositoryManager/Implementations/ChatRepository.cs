using LinqKit;
using Microsoft.Data.SqlTypes;
using Microsoft.EntityFrameworkCore;
using ProSphere.Data.Context;
using ProSphere.Domain.Constants.FileConstants;
using ProSphere.Domain.Entities;
using ProSphere.Features.Chat.Queries.GetAllChatBetweenUsers;
using ProSphere.Features.Chat.Queries.GetAllContactsForUser;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.RepositoryManager.Pagination;
using Supabase.Gotrue;
using System.Linq.Expressions;

namespace ProSphere.RepositoryManager.Implementations
{
    public class ChatRepository : IChatRepository
    {
        private readonly AppDbContext _db;

        public ChatRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<PageSourcePagination<GetAllChatBetweenUsersResponse>> GetMessagesBetweenUsers(string firstUserId, string secondUserId, int pageNumber, int pageSize)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 5) pageSize = 5;

            Expression<Func<Message, bool>> filter = m =>
                (m.SenderId == firstUserId && m.ReceiverId == secondUserId)
                ||
                (m.SenderId == secondUserId && m.ReceiverId == firstUserId);


            IQueryable<Message> query = _db.Messages
                .Where(filter)
                .OrderByDescending(m => m.SentAt);


            var totalRecords = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            var result = query.Select(m => new GetAllChatBetweenUsersResponse
            {
                MessageId = m.Id,
                SenderId = m.SenderId!,
                Message = m.Content,
                ImageURL = m.ImageURL == null ? null : SupabaseConstants.PrefixSupaURL + m.ImageURL,
                SentAt = m.SentAt
            });

            return new PageSourcePagination<GetAllChatBetweenUsersResponse>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                Data = (await result.ToListAsync())
                .OrderBy(m => m.SentAt)
                .ToList()
            };
        }

        public async Task<PageSourcePagination<GetAllContactsForUserResponse>> GetUserContacts(
            string userId, Expression<Func<Conversation, bool>> filter, int pageNumber, int pageSize)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 5) pageSize = 5;

            filter = filter.And(c => c.CreatorId == userId || c.InvestorId == userId);

            IQueryable<Conversation> query = _db.Conversations.Where(filter).OrderByDescending(c => c.LastMessageSentAt);

            var totalRecords = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            var result = query.Select(c => new GetAllContactsForUserResponse
            {
                UserId = c.CreatorId == userId ? c.InvestorId : c.CreatorId,

                UserFullName = c.CreatorId == userId
                            ? c.Investor.FullName
                            : c.Creator.FullName,

                UserImageProfileURL = c.CreatorId == userId
                            ? (c.Investor.ImageProfileURL == null
                                ? null
                                : SupabaseConstants.PrefixSupaURL + c.Investor.ImageProfileURL)
                            : (c.Creator.ImageProfileURL == null
                                ? null
                                : SupabaseConstants.PrefixSupaURL + c.Creator.ImageProfileURL),

                LastMessage = c.LastMessage,

                LastMessageSentAt = c.LastMessageSentAt
            });

            return new PageSourcePagination<GetAllContactsForUserResponse>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                Data = await result.ToListAsync()
            };
        }
    }
}
