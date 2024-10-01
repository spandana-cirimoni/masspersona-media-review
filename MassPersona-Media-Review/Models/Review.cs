// Models/Review.cs
using System.ComponentModel.DataAnnotations;

namespace MassPersonaMediaReview.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public Category Category { get; set; } // e.g., Movie, Book, Game

        [Required]
        public string ReviewText { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateReviewed { get; set; }
    }
}
