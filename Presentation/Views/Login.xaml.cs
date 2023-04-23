using System;
using System.Threading.Tasks;
using System.Windows;
using Business;

namespace Presentation
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class Login : Window
	{
		public Login()
		{
			InitializeComponent();
		}


		private async void OnLoging(object sender, RoutedEventArgs e)
		{
			try
			{
				var id = txtUsuario.Text;
				var pass = txtPassword.Password;
				int idUsuario = int.Parse(id);
				bool isValid = await B_User.LogingAsync(idUsuario, pass);
				if (isValid)
				{
					TempData.idUser = idUsuario;
					var viewHome = new Home();
					this.Hide();
					viewHome.ShowDialog();
					this.ShowDialog();
				}
				else
				{
					MessageBox.Show("Datos Invalidos");
				}
			}catch(Exception ef)
			{
				MessageBox.Show($"Error: {ef.Message}");
			}
			
		}


	}
}
