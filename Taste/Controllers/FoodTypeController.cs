using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taste.DataAccess.Data.Repository.IRepository;

namespace Taste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public FoodTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

       

        [HttpGet]
        public IActionResult GetALL()
        {
            return Json(new {data = _unitOfWork.FoodType.GetAll() });

        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var obj = _unitOfWork.FoodType.GetFirstOrDefault(c => c.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error deleting category" });
            }

            _unitOfWork.FoodType.Remove(obj);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete successfull" });


        }
    }
}