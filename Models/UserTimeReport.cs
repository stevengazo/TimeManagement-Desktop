using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
	public class UserTimeReport
	{
		public int idUser { get; set; }
		public string name { get; set; }
		public string lastname { get; set; }
		public DateTime? date { get; set; }
		public int hours { get; set; }
		public int minutes { get; set; }

	}
}
