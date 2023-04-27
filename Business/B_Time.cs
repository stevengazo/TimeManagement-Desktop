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
