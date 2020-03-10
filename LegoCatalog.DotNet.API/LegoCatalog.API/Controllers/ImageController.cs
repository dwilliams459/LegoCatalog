using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LegoCatalog.API.Controllers
{
    public class ImageController : Controller
    {
        public ImageController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private IConfiguration _configuration;

        // GET: /<controller>/
        [HttpGet]
        [Route("image/{name}")]
        public ActionResult Image(string name)
        {
            try
            {
                string sourcePath = _configuration["IconDirectory"];
                var path = Path.Combine(sourcePath, name);  // Server.MapPath("/Images");
                var image = System.IO.File.OpenRead(path);
                
                return File(image, "image/png");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new EmptyResult(); //
        }
    }
}
