using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Waterer.Api.Models
{
    public class Plant
    {
        [Key]
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public DateTime CreateDate { get; set; }
        
        public int UserId { get; set; }
        

        public User User { get; set; }
        public ICollection<PlantState> PlantStates { get; set; }
        public ICollection<PlantImage> PlantImages { get; set; }
        
    }
}