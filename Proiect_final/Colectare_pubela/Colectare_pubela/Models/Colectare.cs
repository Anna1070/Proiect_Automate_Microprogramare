using System.ComponentModel.DataAnnotations;

namespace Colectare_pubela.Models
{
    public class Colectare
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string TagId { get; set; }

        [Required]
        public DateTime CollectionTime { get; set; }

        //[Required]
        [StringLength(50)]
        public string Address { get; set; }
        public string Latitudine { get; set; }
        public string Longitudine { get; set; }
    }
}
