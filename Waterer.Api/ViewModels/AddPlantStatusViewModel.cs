using System;
using System.ComponentModel.DataAnnotations;

namespace Waterer.Api.ViewModels
{
    public class AddPlantStatusViewModel
    {   
        [Required(ErrorMessage = "Temperatura jest wymagana.")]
        public decimal Temperature { get; set; }

        [Required(ErrorMessage = "Wilgotność jest wymagana.")]
        public decimal Humidity { get; set; }

        [Required]
        public int PlantId { get; set; }
    }
}