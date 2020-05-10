using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taste.DataAccess.Data.Repository.IRepository;

namespace Taste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostingEnvironment;

        public MenuItemController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostingEnvironment = webHostingEnvironment;
        }

       

        [HttpGet]
        public IActionResult GetALL()
        {
            return Json(new {data = _unitOfWork.MenuItem.GetAll(null,null, "Category,FoodType") });

        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var obj = _unitOfWork.MenuItem.GetFirstOrDefault(c => c.Id == id);
                if (obj == null)
                {
                    return Json(new { success = false, message = "Error deleting category" });
                }

                if (obj.Image != null)
                {

                    var imagePath = Path.Combine(_webHostingEnvironment.WebRootPath, obj.Image.TrimStart('\\'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }
                _unitOfWork.MenuItem.Remove(obj);
                _unitOfWork.Save();
            }
            catch(Exception )
            {
                return Json(new { success = false, message = "Error deleting category" });
            }
            return Json(new { success = true, message = "Delete successfull" });


        }
    }
}