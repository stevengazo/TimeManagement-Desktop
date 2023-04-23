using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
	public class TimeItemWithQuantity: TimeItem
	{
		public int Hours { get; set; }
		public int Minutes { get; set; }	
	}
}
