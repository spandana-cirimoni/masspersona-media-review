// Models/Review.cs
using System.ComponentModel.DataAnnotations;

namespace MassPersonaMediaReview.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title cannot be longer than 25 characters.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Category is required.")]
        public Category? Category { get; set; }

        [Required(ErrorMessage = "Review text is required.")]
        [StringLength(1000, ErrorMessage = "Review text cannot be longer than 100 characters.")]
        public string ReviewText { get; set; } = string.Empty;

        [Required(ErrorMessage = "Rating is required.")]
        [Range(1, 5, ErrorMessage = "Rating must be an integer between 1 and 5.")]
        public int Rating { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateReviewed { get; set; }
    }
}
