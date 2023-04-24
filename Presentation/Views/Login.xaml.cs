﻿using System;
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
		private  void OnLoging(object sender, RoutedEventArgs e)
		{
			try
			{
				var id = txtUsuario.Text;
				var pass = txtPassword.Password;
				int idUsuario = int.Parse(id);
				bool isValid = B_User.LogingAsync(idUsuario, pass).Result;
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
		private async Task SaveCredentialsAsync()
		{
			throw new NotImplementedException();
		}
		private async Task LoadCredentialsAsync()
		{
			throw new NotImplementedException();
		}


	}
}
