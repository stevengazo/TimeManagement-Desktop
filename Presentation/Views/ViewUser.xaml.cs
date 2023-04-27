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
		private async void CanExecuteViewTask(object target, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
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
				TasksList = await B_Task.ListTaskItemsAsync(TempData.UserToReview.UserId);
				listViewTaskItems.ItemsSource = TasksList;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private async Task loadDataAsync()
		{
			if(TempData.UserToReview != null)
			{
				txtIdUser.Text = TempData.UserToReview.UserId.ToString();
				txtName.Text = TempData.UserToReview.Name.ToString();
				txtLastName.Text = TempData.UserToReview.LastName.ToString();
				await LoadTasks();
			}
		}
		private async void Seach_Click(object sender, RoutedEventArgs e)
		{
			throw new NotImplementedException();
			try
			{
				/// Validate Data
				
				// Search

				// LoadResults
			}catch(Exception f)
			{
				MessageBox.Show(f.Message);
			}
		}
		private async void Export_Click(object sender, RoutedEventArgs e)
		{
			throw new NotImplementedException();
		}
		private async void ChangeAdminRol_Click(object sender, RoutedEventArgs e)
		{
			throw new NotImplementedException();
		}
		private async void DisableUser_Click(object sender, RoutedEventArgs e)
		{
			throw new NotImplementedException();
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
