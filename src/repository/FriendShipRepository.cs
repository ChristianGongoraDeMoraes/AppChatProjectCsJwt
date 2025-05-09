using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.src.data;
using api.src.dto.FriendShipD;
using api.src.interfaces;
using api.src.model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace api.src.repository
{
    public class FriendShipRepository : IFriendShipRepository
    {
        private readonly ApplicationDbContext _context;
        public FriendShipRepository(ApplicationDbContext context){
            _context = context;
        }

        public async Task<FriendShip?> AddFriend(FriendShipDto req)
        {
            var friend = new FriendShip{
                UserId = req.UserId,
                FriendId = req.FriendId
            };
            
            var existsFriendShip = await _context.FriendShips.AnyAsync(x => x.UserId == req.UserId && x.FriendId == req.FriendId);
            if(existsFriendShip) return null;

            await _context.FriendShips.AddAsync(friend);
            await _context.SaveChangesAsync();

            return friend;
        }

        public async Task<List<FriendShip>> GetAllFriends(string userId)
        {
            var friendShips = _context.FriendShips.AsQueryable();
            
            friendShips = friendShips.Where(x => x.UserId == userId);

            return await friendShips.ToListAsync();
        }
    }
}