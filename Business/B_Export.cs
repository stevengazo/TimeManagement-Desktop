using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DataAccess;
using System.Windows.Media.Animation;
using Microsoft.EntityFrameworkCore;

namespace Models
{
	public static class B_Export
	{
		public static async Task<List<TaskItemReport>> GenerateReportAsync( int Userid =0, int Month = 0)
		{
			try
			{
			
				using TimeDatabaseContext db = new TimeDatabaseContext();
				List<TaskItemReport> result = null;
				if(Userid > -1 && Month >-1)
				{
					var requltQuery = await (	from Tas in db.TaskItems 									   
												join U in db.Users
												on Tas.UserId equals U.UserId

												where (Tas.CreationDate.Year == DateTime.Today.Year) 									   
												&& (U.UserId == Userid)
												&& (Tas.CreationDate.Month == Month)
												select Tas)
												.Include(I=>I.User)
												.Include(i => i.TimeItems)
												.Include(I => I.CategoryItem)
												.Include(I => I.StatusItem)
												.Include(I => I.PriorityItem).ToListAsync();
					var taskToReport = await GenerateStructure(requltQuery);
					return taskToReport;
				}
				else
				{
					return null;
				}				
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		private static async Task< List<TaskItemReport>> GenerateStructure( List<TaskItem> items)
		{
			List<TaskItemReport> list = new List<TaskItemReport>();
			foreach(var item in items)
			{
				var report = new TaskItemReport()
				{
					Title = item.Title,
					Description = item.Description,
					Id = item.TaskItemId,
					Created = item.CreationDate,
					Category = item.CategoryItem.Name,
					Status = item.StatusItem.Name,
					Priority = item.PriorityItem.Name,
					User = item.User.Name					
				};
				report.timeItems=item.TimeItems.ToList();
				var num = 0;
				foreach (var itemi in item.TimeItems)
				{
					var ts = (itemi.StartTime - itemi.EndTime);
					num =  ts.Hours + num;
				}
				report.QuantityOfHours = num;			
				list.Add(report);	
			}
			return list;
		}
	}
}
