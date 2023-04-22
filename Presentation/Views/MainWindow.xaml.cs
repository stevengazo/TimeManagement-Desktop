using System.Threading.Tasks;
using System.Windows;

namespace Presentation
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}


		private void OnLoging(object sender, RoutedEventArgs e)
		{
			var viewHome = new Home();
			this.Hide();
			viewHome.ShowDialog();
			this.ShowDialog();
		}


	}
}
