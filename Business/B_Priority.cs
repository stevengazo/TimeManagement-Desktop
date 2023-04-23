using DataAccess;
using Models;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
	public static class B_Priority
	{
		public static async Task AddPriorityAsync()
		{
			throw new NotImplementedException();
		}
		public static async Task EditPriorityAsync()
		{
			throw new NotImplementedException();
		}
		public static async Task DeletePriorityAsync()
		{
			throw new NotImplementedException();
		}
		public static async Task<List<PriorityItem>> ListPrioritiesAsync()
		{
			try
			{
				using (var db = new TimeDatabaseContext())
				{
					return db.PriorityItems.ToList();
				}
			}catch(Exception f)
			{
				return null;
			}
		}
		public static async Task<PriorityItem> GetPriorityAsync()
		{
			throw new NotImplementedException();
		}
	}
}
