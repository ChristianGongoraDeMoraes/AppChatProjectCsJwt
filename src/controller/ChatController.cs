using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.src.dto.Chat;
using api.src.interfaces;
using api.src.model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.src.controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IChatRepository _chatRepository;
        public ChatController(IChatRepository chatRepository, UserManager<AppUser> userManager)
        {
            _chatRepository = chatRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetChat(string SenderId, string ReceiverId)
        {
            var chat = await _chatRepository.GetAllByReceiverIdAndSenderId(SenderId, ReceiverId);
            
            if (chat == null) return NotFound();

            return Ok(chat);
        }
        [HttpPost]
        public async Task<IActionResult> SendChat([FromBody] ChatDto chatDto)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == chatDto.SenderId);
            var chatModel = new Chat{
                Message = chatDto.Message,
                SenderId = chatDto.SenderId,
                SenderName = user.UserName,
                ReceiverId = chatDto.ReceiverId,
            };

            var chat = await _chatRepository.SendChatAsync(chatModel);
            return Ok(chat);
        }
    }
}