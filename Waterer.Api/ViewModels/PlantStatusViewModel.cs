using System;
using System.ComponentModel.DataAnnotations;

namespace Waterer.Api.ViewModels
{
    public class PlantStatusViewModel
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Temperatura jest wymagana.")]
        public decimal Temperature { get; set; }

        [Required(ErrorMessage = "Wilgotność jest wymagana.")]
        public decimal Humidity { get; set; }

        public DateTime CreateDate { get; set; }

        [Required]
        public int PlantId { get; set; }
    }
}