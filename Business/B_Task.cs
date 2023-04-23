using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Models;
using Microsoft.EntityFrameworkCore;

namespace Business
{
	public static class B_Task
	{
		public static async Task AddTaskItemAsync()
		{
			throw new NotImplementedException();
		}
		public static async Task EditTaskItemAsync()
		{
			throw new NotImplementedException();
		}
		public static async Task DeleteTaskItemAsync()
		{
			throw new NotImplementedException();
		}
		public static async Task<List<TaskItem>> ListTaskItemsAsync()
		{
			try
			{
				using(TimeDatabaseContext db = new())
				{
					return db.TaskItems.Include(C => C.CategoryItem).Include(P => P.PriorityItem).Include(S => S.StatusItem).ToList();
				}
			}catch(Exception f)
			{
				return null;
			}
		}
		public static async Task<TimeItem> GetTaskItemAsync()
		{
			throw new NotImplementedException();
		}
		private static async Task<int> GetTimeItemIdAsync()
		{
			throw new NotImplementedException();
		}
	}
}
