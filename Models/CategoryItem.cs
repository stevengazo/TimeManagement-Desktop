using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
	public class CategoryItem
	{
		[Key]
		[Required]
		//[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CategoryItemId { get; set; }
		public string Name { get;set; }
		public ICollection<TaskItem> TaskItems { get; set;}
	}
}
