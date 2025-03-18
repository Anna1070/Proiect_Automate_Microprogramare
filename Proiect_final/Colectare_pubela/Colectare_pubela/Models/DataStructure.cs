﻿using System.ComponentModel.DataAnnotations;

namespace Colectare_pubela.Models
{
    public class DataStructure
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string TagId { get; set; }

        [Required]
        public DateTime CollectionTime { get; set; }

    }
}
