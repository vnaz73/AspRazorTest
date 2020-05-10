using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Taste.DataAccess.Data.Repository.IRepository;
using Taste.Models.ViewModels;

namespace Taste.Pages.Admin.MenuItem
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment webHostEnvironment;

        public UpsertModel(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            this.unitOfWork = unitOfWork;
            this.webHostEnvironment = webHostEnvironment;
        }
        [BindProperty]
        public MenuItemVM MenuItemObj { get; set; }

        public IActionResult OnGet(int? id)
        {

            MenuItemObj = new MenuItemVM()
            {
                MenuItem = new Models.MenuItem(),
                CategoryList = unitOfWork.Category.GetCategoryListForDropDown(),
                FoodTypeList = unitOfWork.FoodType.GetFoodTypeListForDropDown()
            };
            
            if(id != null)
            {
                MenuItemObj.MenuItem = unitOfWork.MenuItem.GetFirstOrDefault(c => c.Id == id);
                if (MenuItemObj.MenuItem == null) return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            string webHostPath = webHostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            if (!ModelState.IsValid)
            {
                return Page();
            }
            if(MenuItemObj.MenuItem.Id == 0)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(webHostPath, @"images\menuItems");
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                MenuItemObj.MenuItem.Image = @"\images\menuItems\" + fileName + extension;
                this.unitOfWork.MenuItem.Add(MenuItemObj.MenuItem);
            }
            else
            {
                var obj = unitOfWork.MenuItem.Get(MenuItemObj.MenuItem.Id);
                if(files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webHostPath, @"images'menuItems");
                    var extension = Path.GetExtension(files[0].FileName);

                    var imagePath = Path.Combine(webHostPath, obj.Image.TrimStart('\\'));

                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);


                        using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }

                        
                    }
                    else
                    {
                        MenuItemObj.MenuItem.Image = obj.Image;
                    }

                }
                this.unitOfWork.MenuItem.Update(MenuItemObj.MenuItem);
            }
            unitOfWork.Save();
            return Redirect("./Index");
        }
    }
}