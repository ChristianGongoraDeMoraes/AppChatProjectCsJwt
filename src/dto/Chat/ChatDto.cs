using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.src.dto.Chat
{
    public class ChatDto
    {
        public string Message { get; set; } = "";
        public string SenderId { get; set; } = "";
        public string ReceiverId { get; set; } = "";
    }
}