using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace Business
{
	public static class B_Category
	{
		public static async Task AddCategoryAsync()
		{
			throw new NotImplementedException();
		}
		public static async Task<List<CategoryItem>> ListCategoriesAsync()
		{
			try
			{
				using (TimeDatabaseContext db = new())
				{
					return db.CategoryItems.ToList();
				}

			}catch(Exception ex) {
				return null;
			}
		}
		public static async Task EditCategoryAsync()
		{
			throw new NotImplementedException();
		}
		public static async Task DeleteCategoryAsync()
		{
			throw new NotImplementedException();
		}
		public static async Task<CategoryItem> GetCategoryAsync()
		{
			throw new NotImplementedException();
		}
	}
}
