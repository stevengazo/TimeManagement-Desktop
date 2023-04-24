using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Presentation.Views
{
	/// <summary>
	/// Interaction logic for ResumeInformation.xaml
	/// </summary>
	public partial class ResumeInformation : Window
	{
		public ResumeInformation()
		{
			InitializeComponent();
			var SeriesCollection = new SeriesCollection
										{
											new LineSeries
											{
												Values = new ChartValues<double> { 3, 5, 7, 4 }
											},
											new ColumnSeries
											{
												Values = new ChartValues<decimal> { 5, 6, 2, 7 }
											}
										};

			chartItem.Series = SeriesCollection;
		}
	}
}
