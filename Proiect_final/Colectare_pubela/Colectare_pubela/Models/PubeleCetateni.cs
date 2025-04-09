using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Colectare_pubela.Models
{
    public class PubeleCetateni 
    {
        [Key]
        public int Id { get; set; }

       //[Required]
       [ForeignKey("Pubela")]
       public string TagId { get; set; }
       public Pubela? Pubela { get; set; }

        //[Required]
        [ForeignKey("Cetatean")]
        public int IdCetatean { get; set; }
        public Cetatean? Cetatean { get; set; }

        [Required]
        [StringLength(50)]
        public string Address { get; set; }
    }
}
