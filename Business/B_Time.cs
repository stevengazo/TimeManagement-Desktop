using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DataAccess;
using Models;
using Microsoft.EntityFrameworkCore;

namespace Business
{
	public static class B_Time
	{

		public static async Task<Dictionary<string,float>> TimePeerCategory(int monthId, int UserId)
		{
			try
			{
				using TimeDatabaseContext db = new();
				var queryConsult =await  (from T in db.TimeItems
											join Ta in db.TaskItems
											on T.TaskItemId equals Ta.TaskItemId
											join C in db.CategoryItems
											on Ta.CategoryItemId equals C.CategoryItemId
											where (T.StartTime.Month == monthId) && (Ta.UserId == UserId) && (T.StartTime.Year == DateTime.Today.Year)
											select new { T.TimeItemId , C.Name, T.StartTime, T.EndTime }
											).ToListAsync();
				var Categories = queryConsult.DistinctBy(q => q.Name);
				Dictionary<string, float> DicCategoryWithtime = new();
                foreach (var item in Categories)
                {
					TimeSpan totaltime = new();
					var times = queryConsult.Where(Q => Q.Name.Equals(item.Name)).ToList();
                    foreach (var time in times)
                    {
						totaltime = totaltime + ( time.EndTime - time.StartTime );
                    }
					var result = (totaltime.TotalMinutes / 60);
					float timeF = (float) double.Round(result, 2);
					DicCategoryWithtime.Add(item.Name, timeF);
                }
				return DicCategoryWithtime;
            }
            catch(Exception f)
			{
				MessageBox.Show($"Error en TimePeerCategory {f.Message}");
				return null;
			}
		}
		public static async Task<Dictionary<string,float>> TimeByDay(int numberOfMonth, int UserId)
		{
			try
			{
				using TimeDatabaseContext db = new();
				var consult = (from Ti in db.TimeItems
							   join Ta in db.TaskItems
							   on Ti.TaskItemId equals Ta.TaskItemId
							   where (Ta.UserId == UserId) && (Ti.StartTime.Month == numberOfMonth) && (Ti.StartTime.Year == DateTime.Today.Year)
							   select Ti
								).ToList();

				var DaysOfMont = DateTime.DaysInMonth(DateTime.Today.Year, numberOfMonth);
				Dictionary<string, float> DaysOfTheMonth = new();
				for (int i = 0; i < DaysOfMont; i++)
				{
					TimeSpan TimeByDay = new();
					var ListOfRegisterByDay = (from T in consult where T.StartTime.Day == i select T).ToList();
                    foreach (var item in ListOfRegisterByDay)
                    {
						TimeByDay = TimeByDay + (item.EndTime - item.StartTime);
                    }
					var time = (float) TimeByDay.TotalMinutes / 60;
					DaysOfTheMonth.Add(i.ToString(), time);
                }
				return DaysOfTheMonth;
            }
            catch(Exception f)
			{
				MessageBox.Show($"Error en TimeByDay -{f.Message}");
				return null;
			}
		}
		public static async Task<List<TimeItem>> ListTimeItemsAsync(DateTime dateTimeToSearch, int taskId)
		{
			try
			{
				using (TimeDatabaseContext db = new())
				{
					var queryResults = await (from T in db.TaskItems
											  join Tim in db.TimeItems
											  on T.TaskItemId equals Tim.TaskItemId
											  where Tim.StartTime.Date == dateTimeToSearch && T.TaskItemId == taskId
											  select Tim).ToListAsync();
					return queryResults;
				}
			}
			catch (Exception f)
			{
				return null;
			}
		}
		public static async Task<bool> AddTime(TimeItem timeItem)
		{

			try
			{
				int idNumber = await GetLastId();
				timeItem.TimeItemId = idNumber + 1;
				using (TimeDatabaseContext db = new())
				{
					db.TimeItems.Add(timeItem);
					await db.SaveChangesAsync();
					return true;
				}				
			}catch(Exception f)
			{
				MessageBox.Show($"Error: {f.Message}");
				return false;
			}
		}
        public static bool UpdateTime(TimeItem timeItem)
        {

            try
            {
                using (TimeDatabaseContext db = new())
                {
                    db.TimeItems.Update(timeItem);
					db.SaveChanges();
                    return true;
                }
            }
            catch (Exception f)
            {
                MessageBox.Show($"Error: {f.Message}");
                return false;
            }
        }
        public static async Task<bool> DeleteTime(TimeItem timeItem)
		{
			try
			{
				using (TimeDatabaseContext db = new())
				{
					db.TimeItems.Remove(timeItem);
					await db.SaveChangesAsync();
					return true;
				}
			}
			catch (Exception f)
			{
				MessageBox.Show($"Error: {f.Message}");
				return false;
			}
		}
        public static TimeItem GetTimeItem(int id)
        {
            using (var db = new TimeDatabaseContext())
            {
                var element =  (from i in db.TimeItems where i.TimeItemId == id select i).FirstOrDefault();
                return element;
            }
        }
        public static async Task<TimeItem> GetTimeItemAsync(int id)
		{
			using (var db = new TimeDatabaseContext())
			{
				var	element = await (from i in db.TimeItems where i.TimeItemId == id select i).FirstOrDefaultAsync();
				return element ;
			}
		}
		public static async Task<int> GetLastId()
		{
			using (var db = new TimeDatabaseContext())
			{
				int number = 0;
				number = await (from i in db.TimeItems orderby i.TimeItemId descending select i.TimeItemId).FirstOrDefaultAsync();
				return number;
			}
		}
	}
}
