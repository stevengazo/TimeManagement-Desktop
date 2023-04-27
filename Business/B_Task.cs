using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Security.RightsManagement;
using System.Windows;
using System.Diagnostics.Eventing.Reader;

namespace Business
{
	public static class B_Task
	{
		public static async Task<bool> AddTaskItemAsync(TaskItem item)
		{
			try
			{
				var id = await GetLastTaskItemIdAsync();
				using (TimeDatabaseContext db = new())
				{
					item.TaskItemId = id + 1;
					db.TaskItems.Add(item);
					await db.SaveChangesAsync();
					return true;
				}
			}
			catch (Exception f)
			{
				return false;
			}
		}
		public static async Task<bool> EditTaskItemAsync(TaskItem taskItem)
		{
			try
			{;
				using (TimeDatabaseContext db = new())
				{
					db.TaskItems.Update(taskItem);
					await db.SaveChangesAsync();
					return true;
				}
			}
			catch (Exception f)
			{
				return false;
			}
		}
		public static async Task DeleteTaskItemAsync()
		{
			throw new NotImplementedException();
		}
		public static async Task<List<TaskItem>> ListTaskItemsAsync()
		{
			try
			{
				using (TimeDatabaseContext db = new())
				{
					return db.TaskItems.Include(C => C.CategoryItem).Include(P => P.PriorityItem).Include(S => S.StatusItem).ToList();
				}
			} catch (Exception f)
			{
				return null;
			}
		}

		public static async Task<bool> EnableTask(int ifOfTask = 0)
		{
			try
			{
				if (ifOfTask != 0)
				{
					var TaskToDisable = await GetTaskItemAsync(ifOfTask);
					if (TaskToDisable != null)
					{
						using TimeDatabaseContext db = new TimeDatabaseContext();
						TaskToDisable.IsEnable = true;
						db.TaskItems.Update(TaskToDisable);
						await db.SaveChangesAsync();
						return true;
					}
					else
					{
						return false;
					}
				}
				else
				{
					return false;
				}
			}
			catch (Exception f)
			{
				MessageBox.Show(f.Message);
				return false;
			}
		}

		public static async Task<List<TaskItem>> ListTaskItemsAsync(int idUser, bool GetFullList = false)
		{
			try
			{
				using (TimeDatabaseContext db = new())
				{
					if (GetFullList)
					{
						return db.TaskItems.Where(T => T.UserId == idUser).Include(C => C.CategoryItem).Include(P => P.PriorityItem).Include(S => S.StatusItem).ToList();
					}
					else
					{
						return db.TaskItems.Where(T => T.UserId == idUser && T.IsEnable).Include(C => C.CategoryItem).Include(P => P.PriorityItem).Include(S => S.StatusItem).ToList();
					}					
				}
			}
			catch (Exception f)
			{
				return null;
			}
		}

		public static async Task<bool> DisableTask(int ifOfTask= 0)
		{
			try
			{
				if(ifOfTask != 0)
				{
					var TaskToDisable = await GetTaskItemAsync(ifOfTask);
					if(TaskToDisable != null) 
					{
						using TimeDatabaseContext db = new TimeDatabaseContext();
						TaskToDisable.IsEnable = false;
						db.TaskItems.Update(TaskToDisable);
						await db.SaveChangesAsync();
						return true;
					}
					else
					{
						return false;
					}
				}
				else
				{
					return false;
				}
			}catch(Exception f)
			{
				MessageBox.Show(f.Message);
				return false;
			}
		}

		public static async Task<TaskItem> GetTaskItemAsync(int idToSearch)
		{
			try
			{
				using (TimeDatabaseContext db = new())
				{
					var data = await (from T in db.TaskItems orderby T.CreationDate descending where T.TaskItemId == idToSearch select T).Include(C => C.CategoryItem)
						.Include(P => P.PriorityItem)
						.Include(S => S.StatusItem)
						.Include(U => U.User)
						.Include(D=>D.TimeItems).FirstOrDefaultAsync();
					return data;
				}
			}
			catch (Exception f)
			{
				return null;
			}
		}
		private static async Task<int> GetLastTaskItemIdAsync()
		{
			try
			{
				using (TimeDatabaseContext db = new())
				{
					var number =await (from T in db.TaskItems orderby T.TaskItemId descending select T.TaskItemId).FirstOrDefaultAsync();
					return number;
				}
			}
			catch (Exception f)
			{
				return -5;
			}
		}
	}
}
