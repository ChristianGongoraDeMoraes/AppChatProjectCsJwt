using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.src.model
{
    public class FriendShip
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FriendId { get; set; }
    }
}