using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MashTodo.Models;
using MashTodo.Service;

namespace TodoMashWPF.Backend
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoItemService _context;

        public TodoItemsController(TodoItemService context)
        {
            _context = context;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<IEnumerable<TodoItem>> GetTodoItem()
        {
            return await _context.ReadAll();
        }

        //// GET: api/TodoItems/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetTodoItem([FromRoute] Guid id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var todoItem = await _context.TodoItem.FindAsync(id);

        //    if (todoItem == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(todoItem);
        //}

        // PUT: api/TodoItems/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTodoItem([FromRoute] Guid id, [FromBody] TodoItem todoItem)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != todoItem.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(todoItem).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TodoItemExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/TodoItems
        [HttpPost]
        public async Task<IActionResult> PostTodoItem([FromBody] TodoItem todoItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdId = await _context.Create(todoItem.Name);

            return CreatedAtAction("GetTodoItem", new { id = createdId }, todoItem);
        }

        //// DELETE: api/TodoItems/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTodoItem([FromRoute] Guid id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var todoItem = await _context.TodoItem.FindAsync(id);
        //    if (todoItem == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.TodoItem.Remove(todoItem);
        //    await _context.SaveChangesAsync();

        //    return Ok(todoItem);
        //}

        //private bool TodoItemExists(Guid id)
        //{
        //    return _context.TodoItem.Any(e => e.Id == id);
        //}
    }
}