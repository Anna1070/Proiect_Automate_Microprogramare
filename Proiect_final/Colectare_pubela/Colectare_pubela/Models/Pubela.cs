using System.ComponentModel.DataAnnotations;

namespace Colectare_pubela.Models
{
    public class Pubela 
    {
        [Key]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Tag must contain only letters and numbers.")]
        public string TagId { get; set; }
    }
}
