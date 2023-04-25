﻿using Business;
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
		public List<TaskItem> TasksList = new List<TaskItem>();
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


		private async void CanExecuteViewTask(object target, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}
		private async void ViewTaskItem(object target, ExecutedRoutedEventArgs e)
		{	
			var numberOfTask = int.Parse(e.Parameter.ToString());
			TempData.TaskItemId = numberOfTask;
			ViewTaskItem viewTaskItemWindow = new();	
			viewTaskItemWindow.ShowDialog();			
		}

		private async void CleanInputs(object sender, RoutedEventArgs e)
		{
			CleanInputs();
		}
		private async void CleanInputs()
		{
			txtTitle.Text = string.Empty;
			txtDescription.Text = string.Empty;
			cbCategory.SelectedIndex = -1;
			cbPriority.SelectedIndex = -1;
			cbStatus.SelectedIndex = -1;
			dptDate.SelectedDate = DateTime.Today;
		}
		private async void AddTaskItem(object sender, RoutedEventArgs e)
		{
			try
			{
				if (string.IsNullOrEmpty(txtDescription.Text) || string.IsNullOrEmpty(txtTitle.Text))
				{
					MessageBox.Show("Verifique los datos");
				}
				else
				{
					var TaskItem = new TaskItem();
					TaskItem.Title = txtTitle.Text;
					TaskItem.Description = txtDescription.Text;
					TaskItem.CreationDate = (DateTime)dptDate.SelectedDate;
					TaskItem.LastEditionDate = DateTime.Today;
					TaskItem.StatusItemId = (from s in StatusItems where s.Name.Equals(cbStatus.Text) select s.StatusItemId).FirstOrDefault();
					TaskItem.PriorityItemId = (from s in PrioritiesItems where s.Name.Equals(cbPriority.Text) select s.PriorityItemId).FirstOrDefault();
					TaskItem.CategoryItemId = (from s in categoryItems where s.Name.Equals(cbCategory.Text) select s.CategoryItemId).FirstOrDefault();
					TaskItem.IsEnable = true;
					TaskItem.UserId = TempData.idUser;
					var isRegister = await B_Task.AddTaskItemAsync(TaskItem);
					if (isRegister)
					{
						await LoadTasks();
						CleanInputs();
					}
				}
			}catch(Exception f)
			{
				MessageBox.Show($"Error: {f.Message}", "Error Añadir Tarea", MessageBoxButton.OK);
			}
		}
		private void OnPerfomancePage(object sender, RoutedEventArgs e)
		{
			if (TempData.User.IsAdmin)
			{
				PerformanceTaskPage performanceTaskPage = new();
				performanceTaskPage.ShowDialog();
			}
			{
				MessageBox.Show("Debes ser administrador para ingresar a esta función", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}
		private void OnExportPage(object sender, RoutedEventArgs e)
		{
			if (TempData.User.IsAdmin)
			{
				ExportData exportDataWindow = new();
				exportDataWindow.ShowDialog();
			}
			{
				MessageBox.Show("Debes ser administrador para ingresar a esta función", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}
		private void OnAdminPage(object sender, RoutedEventArgs e)
		{
			if (TempData.User.IsAdmin)
			{
				var AdminPageView = new AdminPage();
				AdminPageView.ShowDialog();
			}
			else
			{
				MessageBox.Show("Debes ser administrador para ingresar a esta función", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
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
				TasksList = await B_Task.ListTaskItemsAsync(TempData.User.UserId);
				listViewTaskItems.ItemsSource = TasksList;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void OnResumePage(object sender, RoutedEventArgs r)
		{
			ResumeInformation resumeInformation = new();
			resumeInformation.ShowDialog();
		}
		private void BtnViewTask_Click(object sender, RoutedEventArgs e)
		{
		}
	}
}

