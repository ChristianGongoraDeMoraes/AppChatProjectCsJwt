using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.src.dto.FriendShipD;
using api.src.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.src.controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FriendShipController : ControllerBase
    {
        private readonly IFriendShipRepository _friendShipRepository;
        public FriendShipController(IFriendShipRepository friendShipRepository)
        {
            _friendShipRepository = friendShipRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddFriend([FromBody] FriendShipDto req)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != req.UserId) return BadRequest();

            var res = await _friendShipRepository.AddFriend(req);
            if (res == null) return BadRequest();

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFriends()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return NotFound();

            var res = await _friendShipRepository.GetAllFriends(userId);
            return Ok(res);
        }

        [HttpGet]
        [Route("/requests")]
        public async Task<IActionResult> GetAllFriendsRequests()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return NotFound();

            var res = await _friendShipRepository.GetAllFriendsRequests(userId);
            return Ok(res);
        }


        [HttpDelete]
        public async Task<IActionResult> declineRequestFriend([FromQuery] FriendShipDto req)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != req.UserId) return BadRequest();

            await _friendShipRepository.declineFriend(req);

            return Ok();
        }
    }
}