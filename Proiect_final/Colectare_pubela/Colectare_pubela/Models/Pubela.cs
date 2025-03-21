using System.ComponentModel.DataAnnotations;

namespace Colectare_pubela.Models
{
    public class Pubela 
    {
        [Key]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Tag-ul trebuie să conțină doar litere și cifre.")]
        public string TagId { get; set; }
    }
}
