using System;

namespace Waterer.Api.Models
{
    public class PlantState
    {
        public int Id { get; set; }
        
        public decimal Temperature { get; set; }
        
        public decimal Humidity { get; set; }
        public DateTime CreateDate { get; set; }

        public int PlantId { get; set; }
        
        public Plant Plant { get; set; }
        
        
    }
}