using System;
using System.Net.Mime;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Waterer.Api.Data;
using Waterer.Api.ViewModels;
using Waterer.Api.Models;
using Microsoft.AspNetCore.Http;

namespace Waterer.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlantsController : ControllerBase
    {
        private readonly WatererDBContext _context;

        public PlantsController(WatererDBContext context)
        {
            _context = context;
        }


        // GET api/plants
        [Authorize()]
        [HttpGet]
        public IActionResult GetAll(string sort, int? pageNumber, int? pageSize)
        {
            try
            {
                var plants = _context.Plants.ToList();

                var currPageNumber = pageNumber ?? 1;
                var currPageSize = pageSize ?? plants.Count;

                switch (sort)
                {
                    case "asc":
                        return Ok(plants.OrderBy(p => p.Name));
                    case "desc":
                        return Ok(plants.OrderBy(p => p.Name));
                    default:
                        return Ok(plants);
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // GET api/plants/details/1
        [Authorize()]
        [HttpGet("[action]/{id}")]
        public IActionResult Details(int id)
        {
            try
            {
                var plant = _context.Plants.Find(id);

                if (plant == null) return NotFound("Nie można odnaleźć rośliny.");

                var userName = _context.Users.Find(plant.UserId)?.Name;
                var states = _context.PlantStatuses.Where(s => s.PlantId == plant.Id).Select(s => new PlantStatusViewModel
                {
                    Id = s.Id,
                    Temperature = s.Temperature,
                    Humidity = s.Humidity,
                    CreateDate = s.CreateDate,
                    PlantId = s.PlantId
                }).ToList();
                var images = _context.PlantImages.Where(i => i.PlantId == plant.Id).Select(p => p.Path).ToList();

                var plantDetails = new PlantDetailsViewModel()
                {
                    Id = plant.Id,
                    Name = plant.Name,
                    UserName = userName,
                    Statuses = states,
                    Images = images,
                };

                return Ok(plantDetails);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        // POST api/plants
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Add([FromBody] PlantRequest request)
        {
            // if (!ModelState.IsValid)
            // {
            //     var errors = ModelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToArray();
            //     return BadRequest(errors);
            // }

            try
            {
                var newPlant = new Plant()
                {
                    Name = request.Name,
                    CreateDate = DateTime.Now,
                    UserId = request.UserId
                };

                _context.Plants.Add(newPlant);
                _context.SaveChanges();

                return StatusCode(StatusCodes.Status201Created);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // PUT api/plants/1
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] PlantRequest request)
        {
            // if (!ModelState.IsValid)
            // {
            //     var errors = ModelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToArray();
            //     return BadRequest(errors);
            // }

            try
            {
                var plant = _context.Plants.Find(id);
                // Jeśli nie znaleziono
                if (plant == null) return NotFound("Nie można odnaleźć rośliny.");

                plant.Name = request.Name;

                _context.SaveChanges();

                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // DELETE api/plants/1
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var plant = _context.Plants.Find(id);
                // Jeśli nie znaleziono
                if (plant == null) return NotFound("Nie można odnaleźć rośliny.");

                // Usuń zdjęcia
                var images = _context.PlantImages.Where(i => i.PlantId == plant.Id);
                if (images.Count() <= 0) _context.PlantImages.RemoveRange(images);

                // Usuń statusy
                var statuses = _context.PlantStatuses.Where(s => s.PlantId == plant.Id);
                if (statuses.Count() <= 0) _context.PlantStatuses.RemoveRange(statuses);

                _context.Plants.Remove(plant);

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