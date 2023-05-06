using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace Business
{
	public static class B_Category
	{

		public static async Task<Dictionary<string,int>> QuantityOfTaskByCategory(int IdUser  = 0 , int Month = 0)
		{
			try
			{
				using TimeDatabaseContext db = new();
				var queryResuts = await (from C in db.CategoryItems
									join T in db.TaskItems on
									C.CategoryItemId equals T.CategoryItemId								   
									where T.UserId == IdUser && T.CreationDate.Month == Month && T.CreationDate.Year == DateTime.Today.Year
									select T
								   )
								   .Include(T=>T.CategoryItem)
								   .ToListAsync();
				Dictionary<string, int> QuantityOfTaskByCategoryData = new();
                var listOfCategoriesInTasks= queryResuts.Select(D=>D.CategoryItem).DistinctBy( R=>R.CategoryItemId ).ToList();
                foreach (var Category in listOfCategoriesInTasks)
                {
					var QuantityOfTask = (from R in queryResuts where R.CategoryItemId == Category.CategoryItemId select R).Count();
					QuantityOfTaskByCategoryData.Add(Category.Name, QuantityOfTask);
                }
				return QuantityOfTaskByCategoryData;
            }
            catch(Exception ex)
			{
				MessageBox.Show("Error QuantityOfTask");
				return null;
			}
		}


		public static async Task<List<ChartItem>> GetResumeByMonth()
		{
			var list= new List<ChartItem>();
			list.Add(new ChartItem()
			{
				Category = "sample",
				Number = 52,
			});
			list.Add(new ChartItem()
			{
				Category = "sample 2",
				Number = 10,
			});
			return list;
		}
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
