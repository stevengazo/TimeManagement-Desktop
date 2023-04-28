using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
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
using Business;
using Models;

namespace Presentation.Views
{
	/// <summary>
	/// Lógica de interacción para ExportData.xaml
	/// </summary>
	public partial class ExportData : Window
	{
		Dictionary<int,string> months = new Dictionary<int, string>()
		{		
			{1, "Enero" },
			{2, "Febrero" },
			{3, "Marzo" },
			{4, "Abril" },
			{5, "Mayo" },
			{6, "Junio" },
			{7, "Julio" },
			{8, "Agosto" },
			{9, "Septiembre" },
			{10, "Octubre" },
			{11, "Noviembre" },
			{12, "Diciembre" }
		};
		Dictionary<int, string> usersDic = new Dictionary<int, string>();

		public ExportData()
		{
			InitializeComponent();
			LoadData();
		}
		private async Task LoadData()
		{
			try
			{
				foreach (var item in months)
				{
					cbMonth.Items.Add(item.Value);
				}
				usersDic = await B_User.DicUsersAsync();
				foreach (var item in usersDic)
				{
					cbUsers.Items.Add(item.Value);
				}
				cbUsers.Items.Add("Todos");
				this.Show();

			}
			catch (Exception ex) {
				MessageBox.Show(ex.Message); 
			}
            
		}
	}
}
