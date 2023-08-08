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
using System.ComponentModel;
using Models;

namespace Presentation.Views
{
	/// <summary>
	/// Interaction logic for PerformanceTaskPage.xaml
	/// </summary>
	public partial class PerformanceTask : Window
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

		public PerformanceTask()
		{
			InitializeComponent();
			LoadData();
		}
		private async Task LoadData()
		{
			try
			{
				usersDic = Task.Run(B_User.DicUsersAsync).Result;
				cbUsers.Text = TempData.CurrentUser.Name;
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
				await TimeByDays(monthId, userId);
				await TotalOfTaskByCategory(monthId, userId);
				await TimeByCategory(monthId, userId);
			}
			else
			{
				MessageBox.Show("Verifique los datos");
			}		
		}
		private async Task TotalOfTaskByCategory(int month,int user)
		{
			var QuantityByCategory = await B_Category.QuantityOfTaskByCategory(user, month);
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
				chart1.Series = s1;
				chart1AxisX.Labels = QuantityByCategory.Keys.ToArray();
				Formatter = value => value.ToString("N");
				chart1AxisY.LabelFormatter = Formatter;
			}
			else
			{
				MessageBox.Show("No hay datos con que trabajar");
			}
		}
		private async Task TimeByDays(int month,int user)
		{
			var TimeByDay = await B_Time.TimeByDay(month, user);

			if (TimeByDay.Count > 0)
			{
				SeriesCollection s2 = new SeriesCollection()
					{
						new LineSeries()
						{
							Title=$"Tiempo",
							Values = new ChartValues<float>(TimeByDay.Values),
							PointGeometry = DefaultGeometries.Square,
							PointGeometrySize = 3
						}
					};
				chart2.Series = s2;
				chart2AxisX.Labels = TimeByDay.Keys.ToArray();
				Formatter = value => value.ToString("N");
				chart1AxisY.LabelFormatter = Formatter;
			}
			else
			{
				MessageBox.Show("No hay datos con los que trabajar");
			}
		}
		private async Task TimeByCategory(int month, int user)
		{
			var TimeByCategory = await B_Time.TimePeerCategory(month, user);

			if (TimeByCategory.Count > 0)
			{
				SeriesCollection seriesViews = new SeriesCollection();
                foreach (var item in TimeByCategory)
                {
					var d = new PieSeries
					{
						Title = item.Key,
						Values = new ChartValues<ObservableValue> { new ObservableValue(item.Value) },
						DataLabels = true
					};
					seriesViews.Add(d);					
                }
				ChartPie1.Series = seriesViews;
			}
			else
			{
				MessageBox.Show("No hay datos con los que trabajar");
			}
		}
	}
}
