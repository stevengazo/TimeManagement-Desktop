using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
	public class TimeItem
	{
		[Key]
		[Required]
		public int TimeItemId { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public string Notes { get; set; }
		public TaskItem TaskItem { get; set; }
		public int TaskItemId { get; set; }
	}
}
