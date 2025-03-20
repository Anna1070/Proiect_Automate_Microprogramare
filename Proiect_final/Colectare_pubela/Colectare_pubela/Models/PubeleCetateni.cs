using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Colectare_pubela.Models
{
    public class PubeleCetateni 
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

       [Required]
       [ForeignKey("Pubela")]
       public string TagId { get; set; }
       public Pubela Pubela { get; set; }

        [Required]
        [ForeignKey("Cetatean")]
        public Guid IdCetatean { get; set; }
        public Cetateni Cetatean { get; set; }

        [Required]
        [StringLength(50)]
        public string Address { get; set; }
    }
}
