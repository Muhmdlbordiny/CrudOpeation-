using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Assighment.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public ServiceController(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }
        public IActionResult uploadImage()
        {
            return View();
        }
        [HttpPost]
        public IActionResult uploadImage(IFormFile Image)
        {
            string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images");

            string uniqueFileName = Guid.NewGuid().ToString() + "_" + Image.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                Image.CopyTo(fileStream);
                fileStream.Close();
            }

            return View();
        }
    }
}
