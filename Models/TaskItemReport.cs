using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
	public class TaskItemReport
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }	
		public DateTime? Created { get; set; }
		public string Category { get; set; }	
		public string Priority { get; set; }
		public string Status { get; set; }
		public string User { get; set; }
		public int QuantityOfHours { get; set; }	
		public int QuantityOfDays { get; set; }
		public List<TimeItem> timeItems { get; set; }
	}
}
