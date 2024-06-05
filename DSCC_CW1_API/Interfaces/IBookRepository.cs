using DSCC_CW1_API.Dtos;
using DSCC_CW1_API.Models;

namespace DSCC_CW1_API.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book> GetBookByIdAsync(int bookId);
        Task<Book> CreateBookAsync(Book book);
        Task<Book> UpdateBookAsync(Book book);
        Task DeleteBookAsync(int bookId);
    }
}
