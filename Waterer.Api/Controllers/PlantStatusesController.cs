using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Waterer.Api.Data;
using Waterer.Api.Models;
using Waterer.Api.ViewModels;

namespace Waterer.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlantStatusesController : ControllerBase
    {
        private readonly WatererDBContext _context;

        public PlantStatusesController(WatererDBContext context)
        {
            _context = context;
        }


        // POST api/plantstatuses
        [HttpPost]
        public IActionResult Add([FromBody] AddPlantStatusViewModel model)
        {
            // if (!ModelState.IsValid)
            // {
            //     var errors = ModelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToArray();
            //     return BadRequest(errors);
            // }
            
            try
            {
                var newStatus = new PlantStatus()
                {
                    Temperature = model.Temperature,
                    Humidity = model.Humidity,
                    CreateDate = DateTime.Now,
                    PlantId = model.PlantId
                };

                _context.PlantStatuses.Add(newStatus);
                _context.SaveChanges();

                return StatusCode(StatusCodes.Status201Created);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        // DELETE api/plantstatuses
        [HttpDelete("{id}")]
        [Authorize()]
        public IActionResult Delete(int id)
        {
            try
            {
                var plantStatus = _context.PlantStatuses.Find(id);

                if (plantStatus == null) return NotFound("Nie można odnaleźć statusu.");

                _context.PlantStatuses.Remove(plantStatus);
                _context.SaveChanges();

                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}