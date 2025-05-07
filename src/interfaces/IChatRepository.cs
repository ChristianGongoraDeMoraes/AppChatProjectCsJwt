using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.src.model;

namespace api.src.interfaces
{
    public interface IChatRepository
    {
        Task<List<Chat?>> GetAllByReceiverIdAndSenderId(string SenderId, string ReceiverId);
        Task<Chat> SendChatAsync(Chat ChatModel);
    }
}