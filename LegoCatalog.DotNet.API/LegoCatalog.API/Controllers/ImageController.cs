using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LegoCatalog.API.Controllers
{
    public class ImageController : Controller
    {
        private string sourcePath = "c:\\LegoCatalog\\images";        
        
        // GET: /<controller>/
        [HttpGet]
        [Route("image/{name}")]
        public ActionResult Image(string name)
        {
            try
            {
                var path = Path.Combine(sourcePath, name);  // Server.MapPath("/Images");
                var image = System.IO.File.OpenRead(path);
                return File(image, "image/png");;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}
