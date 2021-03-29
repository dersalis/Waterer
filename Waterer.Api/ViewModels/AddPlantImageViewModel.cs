using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Waterer.Api.ViewModels
{
    public class AddPlantImageViewModel
    {
        [Required(ErrorMessage = "Plik obrazu jest wymagany.")]
        public IFormFile Image { get; set; }
        [Required(ErrorMessage = "Id rośliny jest wymagane.")]
        public int PlantId { get; set; }
        
    }
}