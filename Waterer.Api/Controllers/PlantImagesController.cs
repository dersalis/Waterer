using System.Net.Mime;
using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Waterer.Api.Data;
using Waterer.Api.ViewModels;
using Waterer.Api.Models;
using System.Linq;

namespace Waterer.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlantImagesController : ControllerBase
    {
        private readonly WatererDBContext _context;

        public PlantImagesController(WatererDBContext context)
        {
            _context = context;
        }


        // POST api/plantimages
        [HttpPost]
        public IActionResult Add([FromForm] AddPlantImageViewModel model)
        {
            // if (!ModelState.IsValid)
            // {
            //     var errors = ModelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToArray();
            //     return BadRequest(errors);
            // }
            
            try
            {
                var directory = "images";

                if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

                var fileType = Path.GetExtension(model.Image.FileName);

                if (fileType.ToLower() == ".jpg" || fileType.ToLower() == ".png" || fileType.ToLower() == ".jpeg")
                {
                    var guid = Guid.NewGuid();
                    var filePath = Path.Combine(directory, guid + fileType);

                    if (model.Image != null)
                    {
                        var fileStream = new FileStream(filePath, FileMode.Create);
                        model.Image.CopyTo(fileStream);
                    }

                    var plantImgae = new PlantImage()
                    {
                        Path = filePath,
                        CreateDate = DateTime.Now,
                        PlantId = model.PlantId
                    };

                    _context.PlantImages.Add(plantImgae);
                    _context.SaveChanges();

                    return StatusCode(StatusCodes.Status201Created);
                }
                else
                {
                    return BadRequest("Zły typ pliku.");
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}