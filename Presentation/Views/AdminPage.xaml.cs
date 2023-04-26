using Business;
using Models;
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

		private async void CanExecuteViewUser(object target, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		private async void ViewUserAsync(object target, ExecutedRoutedEventArgs e)
		{
			try
			{
				var id = int.Parse(e.Parameter.ToString());
				TempData.UserToReview = await B_User.GetUserAsync(id);
				ViewUser viewUserWindows = new();
				viewUserWindows.ShowDialog();
				LoadUsersAsync();
			}catch(Exception ex)
			{
				MessageBox.Show("Error", ex.Message, MessageBoxButton.OK);
			}
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
		private async void AddUser_Click(object sender, RoutedEventArgs f)
		{
			try
			{
				bool isValid = await ValidateData();				
				if (isValid)
				{					
					User _user = new() {
						Name = txtName.Text,
						LastName = txtLastName.Text,
						Password = txtPassword.Text,
						UserId = int.Parse(txtIdUser.Text.ToString())
					};
					bool theIdIsDuplicate = await B_User.ExistsId(_user.UserId);
					if (theIdIsDuplicate)
					{
						MessageBox.Show("Error", "El id ya existe en la DB", MessageBoxButton.OK, MessageBoxImage.Error);
					}
					else
					{
						bool isAdded = await B_User.AddUserAsync(_user);
						if (isAdded)
						{
							listViewUsers.ItemsSource = await B_User.ListUsersAsync();
							await CleanInputs();
						}
						else
						{
							MessageBox.Show("Error", "Error al agregar al usuario", MessageBoxButton.OK, MessageBoxImage.Error);
						}
					}
				}
			}catch(Exception v)
			{
				MessageBox.Show(v.Message);
			}		
		}

		private async Task<bool> ValidateData()
		{
			try
			{
				var tryParseID = int.TryParse(txtIdUser.Text,out int _);
				if (!tryParseID)
				{
					return false;
				}
				else if (string.IsNullOrEmpty(txtName.Text))
				{
					return false;
				}
				else if (string.IsNullOrEmpty(txtLastName.Text))
				{
					return false;
				}
				else if (string.IsNullOrEmpty(txtPassword.Text))
				{
					return false;
				}
				else {
					return true;
				}
			}
			catch (Exception v)
			{
				MessageBox.Show(v.Message);
				return false;
			}
		}

		private async Task CleanInputs()
		{
			txtIdUser.Text = string.Empty;
			txtName.Text = string.Empty;
			txtLastName.Text = string.Empty;
			txtPassword.Text = string.Empty;
		}

		
		private void MenuItem_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
        }
    }
}
