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
            if(userId != req.UserId) return BadRequest();

            var res =await _friendShipRepository.AddFriend(req);
            if(res == null) return BadRequest();
            
            return Ok();
        }
    }
}