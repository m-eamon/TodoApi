using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendsController : ControllerBase
    {
        private readonly TodoContext _context;

        public FriendsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Friends
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Friends>>> GetFriends()
        {
            return await _context.FriendItems.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Friends>> FriendsItem(long id)
        {

            var friendItem = await _context.FriendItems.FindAsync(id);

            if (friendItem == null) {
                return NotFound();
            }

            return friendItem;

        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Friends>> CreateNewFriend(Friends friend)
        {
            _context.FriendItems.Add(friend);
            
            await _context.SaveChangesAsync();

            return CreatedAtAction("FriendsItem", new {id = friend.Id}, friend);
        }
    }
}
