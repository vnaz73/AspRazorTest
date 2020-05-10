using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Taste.DataAccess.Data.Repository.IRepository;

namespace Taste.Pages.Admin.FoodType
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;

        public UpsertModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [BindProperty]
        public Models.FoodType FoodTypeObj { get; set; }
        public IActionResult OnGet(int? id)
        {

            FoodTypeObj = new Models.FoodType();

            if (id != null)
            {
                FoodTypeObj = unitOfWork.FoodType.GetFirstOrDefault(c => c.Id == id);
                if (FoodTypeObj == null) return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (FoodTypeObj.Id == 0)
            {
                this.unitOfWork.FoodType.Add(FoodTypeObj);
            }
            else
            {
                this.unitOfWork.FoodType.Update(FoodTypeObj);
            }
            unitOfWork.Save();
            return Redirect("./Index");
        }
    }
}