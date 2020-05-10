using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taste.DataAccess.Data.Repository.IRepository;
using Taste.Models;

namespace Taste.DataAccess.Data.Repository
{
    public class MenuItemRepository : Repository<MenuItem>, IMenuItemRepository
    {
        private readonly ApplicationDbContext _db;

        public MenuItemRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(MenuItem menuItem)
        {
            var obj = _db.MenuItem.FirstOrDefault(m => m.Id == menuItem.Id);

            obj.Name = menuItem.Name;
            obj.CategoryId = menuItem.CategoryId;
            obj.FoodTypeId = menuItem.FoodTypeId;
            obj.Description = menuItem.Description;
            obj.Price = menuItem.Price;
            if(menuItem.Image != null)
            {
                obj.Image = menuItem.Image;
            }
            _db.SaveChanges();
            
        }
    }
}
