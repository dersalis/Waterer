using System.ComponentModel.DataAnnotations;

namespace Waterer.Api.ViewModels
{
    public class PlantRequest
    {
        [Required(ErrorMessage = "Nazwa jest wymagana.")]
        [StringLength(100, ErrorMessage = "Nazwa nie moze być dłuzsza niz 100 znaków.")]
        public string Name { get; set; }
        
        [Required]
        public int UserId { get; set; }
    }
}