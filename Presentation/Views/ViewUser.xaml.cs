using Business;
using Microsoft.VisualBasic;
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
	/// Lógica de interacción para ViewUser.xaml
	/// </summary>
	public partial class ViewUser : Window
	{
		private List<CategoryItem> categoryItems = new();
		private List<StatusItem> StatusItems = new();
		private List<PriorityItem> PrioritiesItems = new();
		public List<TaskItem> TasksList = new List<TaskItem>();
		public ViewUser()
		{
			InitializeComponent();
			loadDataAsync();
			loadCategories();
			LoadPriorities();
		}
		private async void CanModificateViewTask(object target, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}
		private async void CanExecuteViewTask(object target, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}
		private async void ViewEditItem(object target, ExecutedRoutedEventArgs e)
		{
			var numberOfTask = int.Parse(e.Parameter.ToString());
			TempData.TaskItemId = numberOfTask;
			EditTask editTaskWindow = new EditTask();
			editTaskWindow.ShowDialog();
		}
		private async Task LoadPriorities()
		{
			try
			{
				PrioritiesItems = await B_Priority.ListPrioritiesAsync();
				foreach (var item in PrioritiesItems)
				{
					cbPriority.Items.Add(item.Name);
				}
			}
			catch (Exception f)
			{
				MessageBox.Show($"Error: {f.Message}");
				this.Close();
			}
		}
		private async Task loadCategories()
		{
			try
			{
				categoryItems = await B_Category.ListCategoriesAsync();
				foreach (var item in categoryItems)
				{
					cbCategory.Items.Add(item.Name);
				}
			}
			catch (Exception f)
			{
				MessageBox.Show($"Error {f.Message}");
				this.Close();
			}
		}
		private async void ViewTaskItem(object target, ExecutedRoutedEventArgs e)
		{
			var numberOfTask = int.Parse(e.Parameter.ToString());
			TempData.TaskItemId = numberOfTask;
			ViewTaskItem viewTaskItemWindow = new();
			viewTaskItemWindow.ShowDialog();
		}
		private async Task LoadTasks()
		{
			try
			{
				TasksList = await B_Task.ListTaskItemsAsync(TempData.UserToReview.UserId, true);
				listViewTaskItems.ItemsSource = TasksList;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private async Task loadDataAsync()
		{
			if (TempData.UserToReview != null)
			{
				txtIdUser.Text = TempData.UserToReview.UserId.ToString();
				txtName.Text = TempData.UserToReview.Name.ToString();
				txtLastName.Text = TempData.UserToReview.LastName.ToString();
				if (TempData.UserToReview.IsEnable)
				{
					btnLoginState.Content = "Desactivar Usuario";
				}
				else
				{
					btnLoginState.Content = "Activar Usuario";
				}

				if (TempData.UserToReview.IsAdmin)
				{
					btnAdminState.Content = "Desactivar rol";
				}
				else
				{
					btnAdminState.Content = "Activar rol";
				}
				await LoadTasks();
			}
		}
		private async void Seach_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Opcion no disponible");
	
		}
		private async void Export_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Opcion no disponible");
			
		}
		private async void ChangeAdminRol_Click(object sender, RoutedEventArgs e)
		{
			if (TempData.UserToReview.IsAdmin)
			{
				var isDisable = await B_User.AdminRolStateAsync(TempData.UserToReview.UserId, false);
				if (isDisable == true)
				{
					MessageBox.Show("Perfil de Administrador eliminado (no podra hacer cambios)", "Informacion", MessageBoxButton.OK, MessageBoxImage.Exclamation);
					btnAdminState.Content = "Añadir rol";
					TempData.UserToReview.IsAdmin = false;
				}
			}
			else
			{
				var isDisable = await B_User.AdminRolStateAsync(TempData.UserToReview.UserId, true);
				if (isDisable == true)
				{
					MessageBox.Show("Perfil de adminsitrador agregado. Podra realizar cambios a los usuarios", "Informacion", MessageBoxButton.OK, MessageBoxImage.Exclamation);
					btnAdminState.Content = "Quitar Rol";
					TempData.UserToReview.IsAdmin = true;
				}
			}
		}
		private async void ChangePassword_Click(object sender, RoutedEventArgs e)
		{
			var Password = Interaction.InputBox ("Digite la contraseña","Cambio Contraseña","");
			if (!string.IsNullOrEmpty(Password))
			{
				bool isChanged = await B_User.ChangePasswordAsync(TempData.UserToReview.UserId, Password);
				if (isChanged == true)
				{
					MessageBox.Show("Contraseña cambiada");
				}
			}
		}
		private async void DisableUser_Click(object sender, RoutedEventArgs e)
		{
			if (TempData.UserToReview.IsEnable)
			{
				var isDisable = await B_User.LoginStateAsync(TempData.UserToReview.UserId, false);
				if (isDisable == true)
				{
					MessageBox.Show("Usuario Desactivado", "Informacion", MessageBoxButton.OK, MessageBoxImage.Exclamation);
					btnLoginState.Content = "Activar Usuario";
					TempData.UserToReview.IsEnable = false;
				}
			}
			else
			{
				var isDisable = await B_User.LoginStateAsync(TempData.UserToReview.UserId, true);
				if (isDisable == true)
				{
					MessageBox.Show("Usuario Activado", "Informacion", MessageBoxButton.OK, MessageBoxImage.Exclamation);
					btnLoginState.Content = "Desactivar Usuario";
					TempData.UserToReview.IsEnable = true;
				}
			}
		}
		private async void EditUser_Click(object sender, RoutedEventArgs e)
		{
			throw new NotImplementedException();
		}
		private async Task<bool> ValidateData()
		{
			throw new NotImplementedException();
		}
	}
}
