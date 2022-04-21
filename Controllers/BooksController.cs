using learn_dot_net.Models;
using learn_dot_net.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace learn_dot_net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _bookRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            return await _bookRepository.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> PostBooks([FromBody] Book book)
        {
            var createdBook = await _bookRepository.Create(book);
            return CreatedAtAction(nameof(GetBooks), new { id = createdBook.Id, createdBook });
        }

        [HttpPut]
        public async Task<ActionResult> PutBook(int id, [FromBody] Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            await _bookRepository.Update(book);
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteBook(int id)
        {

            var book = await _bookRepository.Get(id);
            if (book == null)
            {
                return NotFound();
            }
            await _bookRepository.Delete(book.Id);
            return NoContent();
        }
    }
}