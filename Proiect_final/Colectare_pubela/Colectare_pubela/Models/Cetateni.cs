using System.ComponentModel.DataAnnotations;

namespace Colectare_pubela.Models
{
    public class Cetateni
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "CNP trebuie sa aiba exact 13 caractere.")]
        public string CNP { get; set; }
    }
}
