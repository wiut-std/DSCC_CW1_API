using AutoMapper;
using DSCC_CW1_API.Dtos;
using DSCC_CW1_API.Interfaces;
using DSCC_CW1_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DSCC_CW1_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetAllBooks()
        {
            var books = await _bookRepository.GetAllBooksAsync();
            var bookDtos = _mapper.Map<IEnumerable<BookDto>>(books);
            return Ok(bookDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBookById(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            var bookDto = _mapper.Map<BookDto>(book);
            return Ok(bookDto);
        }

        [HttpPost]
        public async Task<ActionResult<BookDto>> CreateBook([FromBody] BookDto bookDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var book = _mapper.Map<Book>(bookDto);
            var createdBook = await _bookRepository.CreateBookAsync(book);
            var createdBookDto = _mapper.Map<BookDto>(createdBook);
            return CreatedAtAction(nameof(GetBookById), new { id = createdBookDto.BookId }, createdBookDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BookDto>> UpdateBook(int id, [FromBody] BookDto bookDto)
        {
            if (id != bookDto.BookId)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var bookToUpdate = await _bookRepository.GetBookByIdAsync(id);
            if (bookToUpdate == null)
            {
                return NotFound();
            }
            _mapper.Map(bookDto, bookToUpdate);
            await _bookRepository.UpdateBookAsync(bookToUpdate);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            await _bookRepository.DeleteBookAsync(id);
            return NoContent();
        }
    }
}
