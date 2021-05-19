using System.ComponentModel.DataAnnotations;

namespace BlogApi.DTOs
{
    public class BlogPost
    {
        public long Id { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 3)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$", ErrorMessage = "Should start from capital letter and consist only of basic characters.")]
        public string Title { get; set; }

        [StringLength(4096)]
        public string Description { get; set; }
    }
}
