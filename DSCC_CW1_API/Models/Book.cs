﻿namespace DSCC_CW1_API.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public int YearPublished { get; set; }

        // Foreign key
        public int AuthorId { get; set; }

        // Navigation property
        public Author Author { get; set; }
    }
}
