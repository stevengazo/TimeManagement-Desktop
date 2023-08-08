using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
	public class TaskItem
	{
		[Key]
		[Required]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int TaskItemId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
        public int Unit { get; set; }
        public DateTime CreationDate { get; set; }
		public DateTime LastEditionDate { get; set; }
		public bool IsEnable { get; set; }	
		public CategoryItem CategoryItem { get; set; }
		public int CategoryItemId { get; set; }
		public StatusItem StatusItem { get; set; }
		public int StatusItemId { get; set; }
		public User User { get; set; }
		public int UserId { get; set; }
		public PriorityItem PriorityItem { get; set; }
		public int PriorityItemId { get; set; }
		public ICollection<TimeItem> TimeItems { get; set; }

	}
}
