using DataAccess;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
	public static class B_Status
	{
		public static async Task AddStatusAsync()
		{
			throw new NotImplementedException();
		}
		public static async Task EditStatusAsync()
		{
			throw new NotImplementedException();
		}
		public static async Task DeleteStatusAsync()
		{
			throw new NotImplementedException();
		}
		public static async Task<List<StatusItem>> ListStatusAsync()
		{
			try
			{
				using (var db = new TimeDatabaseContext())
				{
					return db.StatusItems.ToList();
				}
			}
			catch (Exception f)
			{
				return null;
			}
		}
		public static async Task<StatusItem> GetStatusItemAsync()
		{
			throw new NotImplementedException();
		}
	}
}
