using Business;
using Models;
using Presentation.Views;
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

namespace Presentation
{
	/// <summary>
	/// Interaction logic for Home.xaml
	/// </summary>
	public partial class Home : Window
	{
		private List<TaskItem> TasksList = new List<TaskItem>();
		private List<CategoryItem> categoryItems = new();
		private List<StatusItem> StatusItems = new();
		private List<PriorityItem> PrioritiesItems = new();
		public Home()
		{
			InitializeComponent();
			LoadTasks();
			loadCategories();
			LoadStatus();
			LoadPriorities();
		}
		private void OnAdminPage(object sender, RoutedEventArgs e)
		{
			var AdminPageView = new AdminPage();
			this.Hide();
			AdminPageView.ShowDialog();
			this.ShowDialog();			
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
		private async Task LoadStatus()
		{
			try
			{
				StatusItems = await B_Status.ListStatusAsync();
				foreach (var item in StatusItems)
				{
					cbStatus.Items.Add(item.Name);
				}
			}catch(Exception f)
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

			}catch(Exception f)
			{
				MessageBox.Show($"Error {f.Message}");
				this.Close();
			}
		}
		private async Task LoadTasks()
		{
			try
			{
				TasksList = await B_Task.ListTaskItemsAsync();
				if (TasksList.Count > 0)
				{
					listViewTaskItems.ItemsSource = TasksList;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void BtnViewTask_Click(object sender, RoutedEventArgs e)
		{
			ViewTaskItem viewTaskItemWindow = new();
			viewTaskItemWindow.ShowInTaskbar = false;
			this.Hide();
			viewTaskItemWindow.ShowDialog();
			this.Show();
		}
	}
}

