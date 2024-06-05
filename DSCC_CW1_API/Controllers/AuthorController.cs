using AutoMapper;
using DSCC_CW1_API.Dtos;
using DSCC_CW1_API.Interfaces;
using DSCC_CW1_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DSCC_CW1_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorController(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAllAuthors()
        {
            var authors = await _authorRepository.GetAllAuthorsAsync();
            var authorDtos = _mapper.Map<IEnumerable<AuthorDto>>(authors);
            return Ok(authorDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetAuthorById(int id)
        {
            var author = await _authorRepository.GetAuthorByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            var authorDto = _mapper.Map<AuthorDto>(author);
            return Ok(authorDto);
        }

        [HttpPost]
        public async Task<ActionResult<AuthorDto>> CreateAuthor([FromBody] AuthorDto authorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var author = _mapper.Map<Author>(authorDto);
            var createdAuthor = await _authorRepository.CreateAuthorAsync(author);
            var createdAuthorDto = _mapper.Map<AuthorDto>(createdAuthor);
            return CreatedAtAction(nameof(GetAuthorById), new { id = createdAuthorDto.AuthorId }, createdAuthorDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AuthorDto>> UpdateAuthor(int id, [FromBody] AuthorDto authorDto)
        {
            if (id != authorDto.AuthorId)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var authorToUpdate = await _authorRepository.GetAuthorByIdAsync(id);
            if (authorToUpdate == null)
            {
                return NotFound();
            }
            _mapper.Map(authorDto, authorToUpdate);
            await _authorRepository.UpdateAuthorAsync(authorToUpdate);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _authorRepository.GetAuthorByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            await _authorRepository.DeleteAuthorAsync(id);
            return NoContent();
        }
    }
}
