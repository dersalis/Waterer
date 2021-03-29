using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Waterer.Api.Models
{
    public class Plant
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Nazwa jest wymagana.")]
        [StringLength(100, ErrorMessage = "Nazwa nie moze być dłuzsza niz 100 znaków.")]
        public string Name { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }
        
        [Required]
        public int UserId { get; set; }
        

        public User User { get; set; }
        public ICollection<PlantStatus> PlantStatuses { get; set; }
        public ICollection<PlantImage> PlantImages { get; set; }
    }
}