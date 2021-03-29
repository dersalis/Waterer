using System.Collections;
using System.Collections.Generic;
using Waterer.Api.Models;

namespace Waterer.Api.ViewModels
{
    public class PlantDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public IEnumerable<PlantStatusViewModel> Statuses { get; set; }
        public IEnumerable<string> Images { get; set; }
    }
}