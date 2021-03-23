using System;

namespace Waterer.Api.Models
{
    public class PlantImage
    {
        public int Id { get; set; }
        
        public string Path { get; set; }
        
        
        public DateTime CreateDate { get; set; }

        public int PlantId { get; set; }
        
        public Plant Plant { get; set; }
    }
}