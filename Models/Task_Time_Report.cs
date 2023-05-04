using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
	public class Task_Time_Report
	{
		public int TimeItemId { get; set; }	
		public int TaskItemId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Priority { get; set; }
		public string Type { get; set; }
		public string UserName { get; set; }
		public int Hours { get; set; }
		public int Minutes { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public string notes { get; set; }

	}
}
