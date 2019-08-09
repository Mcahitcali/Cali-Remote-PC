using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cali_Remote_Pc.Entity;
using Cali_Remote_Pc.Entity.DBContext;
using Action = Cali_Remote_Pc.Entity.Action;

namespace Cali_Remote_Pc.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActionsController : ControllerBase
    {
        private readonly CrpDBContext _context;

        public ActionsController(CrpDBContext context)
        {
            _context = context;
        }

        // GET: api/Actions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Entity.Action>>> GetActions()
        {
            return await _context.Actions.ToListAsync();
        }

        // GET: api/Actions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Entity.Action>> GetAction(Guid id)
        {
            var action = await _context.Actions.FindAsync(id);

            if (action == null)
            {
                return NotFound();
            }

            return action;
        }

        // PUT: api/Actions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAction(Guid id, Entity.Action action)
        {
            if (id != action.Id)
            {
                return BadRequest();
            }

            _context.Entry(action).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Actions
        [HttpPost]
        public async Task<ActionResult<Action>> PostAction(Action action)
        {
            _context.Actions.Add(action);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAction", new { id = action.Id }, action);
        }

        // DELETE: api/Actions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Action>> DeleteAction(Guid id)
        {
            var action = await _context.Actions.FindAsync(id);
            if (action == null)
            {
                return NotFound();
            }

            _context.Actions.Remove(action);
            await _context.SaveChangesAsync();

            return action;
        }

        private bool ActionExists(Guid id)
        {
            return _context.Actions.Any(e => e.Id == id);
        }
    }
}
