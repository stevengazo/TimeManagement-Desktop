using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
	public class User
	{
		[Key]
		[Required]
		public int UserId { get; set; }
		public string Password { get; set; }
		public string Name { get; set; }
		public string LastName { get; set; }
		public bool IsEnable { get; set; }
		public bool IsAdmin { get; set; }
		public bool IsArchive { get; set; }
		public ICollection<TaskItem> TaskItems { get; set; }
	}
}
