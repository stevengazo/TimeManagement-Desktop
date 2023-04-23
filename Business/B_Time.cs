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
