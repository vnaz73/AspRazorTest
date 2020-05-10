using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Taste.DataAccess.Data.Repository.IRepository;

namespace Taste.Pages.Admin.Category
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;

        public UpsertModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [BindProperty]
        public Models.Category CategoryObj { get; set; }
        public IActionResult OnGet(int? id)
        {

            CategoryObj = new Models.Category();
            
            if(id != null)
            {
                CategoryObj = unitOfWork.Category.GetFirstOrDefault(c => c.Id == id);
                if (CategoryObj == null) return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if(CategoryObj.Id == 0)
            {
                this.unitOfWork.Category.Add(CategoryObj);
            }
            else
            {
                this.unitOfWork.Category.Update(CategoryObj);
            }
            unitOfWork.Save();
            return Redirect("./Index");
        }
    }
}