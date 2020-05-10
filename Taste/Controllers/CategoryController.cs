using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Taste.DataAccess.Data.Repository.IRepository;
using Taste.Utility;

namespace Taste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = SD.ManagerRole)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {

            return Json(new { data = _unitOfWork.Category.GetAll() });
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var obj = _unitOfWork.Category.GetFirstOrDefault(c => c.Id == id);
            if(obj == null)
            {
                return Json(new {success = false, message=  "Error deleting category"});
            }

            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete successfull" });


        }
    }
}