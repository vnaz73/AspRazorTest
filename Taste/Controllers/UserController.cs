using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Taste.DataAccess.Data.Repository.IRepository;

namespace Taste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {

            return Json(new { data = _unitOfWork.ApplicationUser.GetAll() });
        }
        [HttpPost]
        public IActionResult Lock([FromBody]string id)
        {
            var obj = _unitOfWork.ApplicationUser.GetFirstOrDefault(c => c.Id == id);
            if(obj == null)
            {
                return Json(new {success = false, message=  "Error deleting locking/unlocking"});
            }
            if(obj.LockoutEnd != null && obj.LockoutEnd > DateTime.Now)
            {
                obj.LockoutEnd = DateTime.Now;
            }
            else
            {
                obj.LockoutEnd = DateTime.Now.AddYears(100);
            }
            
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete locking/unlocking" });


        }
    }
}