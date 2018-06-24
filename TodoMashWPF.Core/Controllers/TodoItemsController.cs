using MashTodo.Models;
using MashTodo.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        //PATCH: api/TodoItems/5
        [HttpPatch]
        public async Task<IActionResult> PatchTodoItem([FromBody] TodoItem todoItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (todoItem.Id == Guid.Empty)
            {
                return BadRequest();
            }

            var foundItem = await _context.Find(todoItem.Id);
            if (foundItem == null)
                return NotFound($"Item with ID {todoItem.Id} was not found");
            await _context.Update(todoItem);
            return Ok();
        }

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
    }
}