using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DataAccess;
using System.Windows.Media.Animation;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace Models
{
	public static class B_Export
	{
		public static async Task<List<Task_Time_Report>> ReportByDayWorkedAsync( DateTime? dateToSearch)
		{
			try
			{
				using TimeDatabaseContext db = new TimeDatabaseContext();


				var queryResults = await (from task in db.TaskItems
										  join time in db.TimeItems
										  on task.TaskItemId equals time.TaskItemId
										  where time.StartTime.Date == dateToSearch && time.EndTime.Date == dateToSearch
										  orderby task.UserId
										  select time).Include(time=>time.TaskItem)
												.Include(time => time.TaskItem.CategoryItem)
												.Include(time => time.TaskItem.PriorityItem)
												.Include(time => time.TaskItem.User)												
												.ToListAsync();
				List<Task_Time_Report> time_Reports = new List<Task_Time_Report>();
                foreach (var item in queryResults)
                {
					TimeSpan timespanTMP = item.EndTime - item.StartTime;
					Task_Time_Report _Time_Report = new()
					{
						TimeItemId = item.TimeItemId,
						TaskItemId = item.TaskItemId,
						Title = item.TaskItem.Title,
						Description = item.TaskItem.Description,
						Priority = item.TaskItem.PriorityItem.Name,
						Type = item.TaskItem.CategoryItem.Name,
						UserName = $"{item.TaskItem.User.LastName} {item.TaskItem.User.Name}",
						Hours = timespanTMP.Hours,
						Minutes = timespanTMP.Minutes,
						StartTime = item.StartTime,
						EndTime = item.EndTime,
						notes = item.Notes
					};
					time_Reports.Add(_Time_Report);	
                }
                return time_Reports;

			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
				return null;
			}
		}

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
				}else if (Userid== -1 && Month > -1){
					var requltQuery = await (from Tas in db.TaskItems											
											 where (Tas.CreationDate.Year == DateTime.Today.Year)											 
											 && (Tas.CreationDate.Month == Month)
											 select Tas)
												.Include(I => I.User)
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
				TimeSpan num = new();
				foreach (var itemi in item.TimeItems)
				{
					var ts = (itemi.EndTime - itemi.StartTime);
					num =  ts + num;
				}
				report.QuantityOfHours = num.Hours;			
				list.Add(report);	
			}
			return list;
		}
	}
}
