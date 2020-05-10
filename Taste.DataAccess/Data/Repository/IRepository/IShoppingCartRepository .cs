using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using Taste.Models;

namespace Taste.DataAccess.Data.Repository.IRepository
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        public int IncrementCount(ShoppingCart shoppingCart, int count);
        public int DecrementCount(ShoppingCart shoppingCart, int count);
    }
}
