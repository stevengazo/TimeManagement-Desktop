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
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Shapes;
using LiveCharts.Defaults; //Contains the already defined types
using Business;
using System.Net.WebSockets;

namespace Presentation.Views
{
	/// <summary>
	/// Interaction logic for PerformanceTaskPage.xaml
	/// </summary>
	public partial class PerformanceTaskPage : Window
	{
		public Func<double, string> Formatter { get; set; }
		public SeriesCollection s1 { get; set; }

		Dictionary<int, string> months = new Dictionary<int, string>()
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

		public PerformanceTaskPage()
		{
			InitializeComponent();
			LoadData();
		}

		private async Task LoadData()
		{
			try
			{
				usersDic = Task.Run(B_User.DicUsersAsync).Result;
				cbUsers.ItemsSource = usersDic.Values;
				cbmonths.ItemsSource = months.Values;
			}catch(Exception f)
			{
				MessageBox.Show(f.Message);
			}

        }

		private async void Search_Click(object sender, RoutedEventArgs e)
		{
			if(!string.IsNullOrEmpty(cbmonths.Text) && !string.IsNullOrEmpty(cbUsers.Text))
			{
				int userId = (from U in usersDic where U.Value == cbUsers.Text select U.Key).First();
				int monthId = (from M in months where M.Value == cbmonths.Text select M.Key).First();

				var QuantityByCategory = await B_Category.QuantityOfTaskByCategory(userId, monthId);
				if (QuantityByCategory.Count > 0)
				{

					SeriesCollection s1 = new()
					{
						new ColumnSeries()
						{
							Title= $"Categorias - {DateTime.Today.ToString("yyyy")}",
							Values = new ChartValues<int>(QuantityByCategory.Values)
						}
					};
					string[] Labels = QuantityByCategory.Keys.ToArray();
					chart1.Series = s1;
					chart1AxisX.Labels = Labels;
					Formatter = value => value.ToString("N");
					chart1AxisY.LabelFormatter = Formatter;
				}
			}
		
		}
	}
}
