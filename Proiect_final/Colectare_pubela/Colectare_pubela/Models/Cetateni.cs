using System.ComponentModel.DataAnnotations;

namespace Colectare_pubela.Models
{
    public class Cetateni
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Name cannot be empty!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must contain at least 2 characters.")]
        [RegularExpression(@"^[a-zA-ZăâîșțĂÂÎȘȚ\s]+$", ErrorMessage = "Name can only contain letters.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Surame cannot be empty!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Surname must contain at least 2 characters.")]
        [RegularExpression(@"^[a-zA-ZăâîșțĂÂÎȘȚ\s]+$", ErrorMessage = "Surname can only contain letters.")]
        public required string Surname { get; set; }

        [Required(ErrorMessage = "Email cannot be empty!")]
        [EmailAddress(ErrorMessage = "Invalid Email address format!")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "CNP cannot be empty!")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "The CNP must have exactly 13 characters.")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "CNP cannot contain letters.")]
        public required string CNP { get; set; }
    }
}
