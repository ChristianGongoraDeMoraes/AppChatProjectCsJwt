using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.src.data;
using api.src.interfaces;
using api.src.model;
using Microsoft.EntityFrameworkCore;

namespace api.src.repository
{
    public class ChatRepository : IChatRepository
    {
        private readonly ApplicationDbContext _context;
        public ChatRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Chat?>> GetAllByReceiverIdAndSenderId(string SenderId, string ReceiverId)
        {
            var chats = _context.Chats.AsQueryable();
            chats = chats.Where(c => c.SenderId == SenderId && c.ReceiverId == ReceiverId ||
                                    c.ReceiverId == SenderId && c.SenderId == ReceiverId);
            chats = chats.OrderBy(c => c.Sended);
            
            if (chats == null) return null;

            return await chats.ToListAsync();
        }

        public async Task<Chat> SendChatAsync(Chat ChatModel)
        {
            await _context.Chats.AddAsync(ChatModel);
            await _context.SaveChangesAsync();
            return ChatModel;
        }
    }
}