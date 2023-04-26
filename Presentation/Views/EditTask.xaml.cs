﻿using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Presentation.Views
{
	/// <summary>
	/// Lógica de interacción para EditTask.xaml
	/// </summary>
	public partial class EditTask : Window
	{
		public EditTask()
		{
			InitializeComponent();
			LoadDataAsync();
		}

		private async Task LoadDataAsync()
		{
			try
			{
				if (!TempData.CurrentUser.IsAdmin)
				{
					datePickerCreationDate.IsEnabled = false;					
					cbEmployee.IsEnabled = false;
				}
			}
			catch (Exception f)
			{
				MessageBox.Show(f.Message);
				this.Close();
			}
		}
	}
}