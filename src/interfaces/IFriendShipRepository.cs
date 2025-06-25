using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.src.dto;
using api.src.dto.FriendShipD;
using api.src.model;

namespace api.src.interfaces
{
    public interface IFriendShipRepository
    {
        Task<FriendShip?> AddFriend(FriendShipDto req);
        Task<List<UserDto>> GetAllFriends(string userId);
        Task<List<UserDto>> GetAllFriendsRequests(string userId);
        Task declineFriend(FriendShipDto req);
    }
}