using DSCC_CW1_API.Data;
using DSCC_CW1_API.Interfaces;
using DSCC_CW1_API.Models;
using Microsoft.EntityFrameworkCore;

namespace DSCC_CW1_API.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<Author> GetAuthorByIdAsync(int authorId)
        {
            return await _context.Authors.FindAsync(authorId);
        }

        public async Task<Author> CreateAuthorAsync(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task<Author> UpdateAuthorAsync(Author author)
        {
            _context.Authors.Update(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task DeleteAuthorAsync(int authorId)
        {
            var author = await _context.Authors.FindAsync(authorId);
            if (author != null)
            {
                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();
            }
        }
    }
}
