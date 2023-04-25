using Business;
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

namespace Presentation.Views
{
	/// <summary>
	/// Interaction logic for AdminPage.xaml
	/// </summary>
	public partial class AdminPage : Window
	{
		public AdminPage()
		{
			InitializeComponent();
			LoadUsersAsync();
		}

		private async void LoadUsersAsync()
		{
			try
			{
				listViewUsers.ItemsSource = await B_User.ListUsersAsync();
			}catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void MenuItem_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
        }
    }
}
