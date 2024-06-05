namespace DSCC_CW1_API.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }

        // Navigation property
        public ICollection<Book> Books { get; set; }
    }
}
