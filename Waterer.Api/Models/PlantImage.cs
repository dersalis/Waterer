using System;
using System.ComponentModel.DataAnnotations;

namespace Waterer.Api.Models
{
    public class PlantImage
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Nazwa jest wymagana.")]
        [StringLength(256)]
        public string Path { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }

        [Required]
        public int PlantId { get; set; }
        
        public Plant Plant { get; set; }
    }
}