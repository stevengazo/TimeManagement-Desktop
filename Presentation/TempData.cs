﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
	internal static class TempData
	{
		internal static int idUser { get; set; } 
		internal static int TaskItemId { get; set; }
		internal static User CurrentUser { get; set; }	
		internal static User UserToReview { get; set;}
		internal static int TimeItemId { get; set; }
	}
}
