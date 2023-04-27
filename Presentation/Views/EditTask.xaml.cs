using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using Models;
using Business;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace Presentation.Views
{
	/// <summary>
	/// Lógica de interacción para EditTask.xaml
	/// </summary>
	public partial class EditTask : Window
	{
		private List<CategoryItem> categoryItems { get;set; }
		private List<PriorityItem> priorityItems { get; set; }
		private List<StatusItem> statusItems { get; set; }
		private Dictionary<int, string> usersDic { get; set; }
		private TaskItem TaskItem { get; set; }	
		public EditTask()
		{
			InitializeComponent();
			LoadDefaultValues();
			LoadDataAsync();
		}
		private async Task LoadDefaultValues()
		{
			categoryItems = await B_Category.ListCategoriesAsync();
			foreach (var category in categoryItems) { 
				cbCategory.Items.Add(category.Name);
			}
			priorityItems = await B_Priority.ListPrioritiesAsync();
			foreach (var prior in priorityItems)
			{
				cbPriority.Items.Add(prior.Name);
			}
			usersDic = await B_User.DicUsersAsync();
			foreach (var user in usersDic)
			{
				cbEmployee.Items.Add(user.Value);
			}
			statusItems = await B_Status.ListStatusAsync();
			foreach (var S in statusItems)
			{
				cbStatus.Items.Add(S.Name);
			}
		}
		private async Task LoadDataAsync()
		{
			try
			{
				if (!TempData.CurrentUser.IsAdmin)
				{
					datePickerCreationDate.IsEnabled = false;					
					cbEmployee.IsEnabled = false;
				}

				TaskItem = await B_Task.GetTaskItemAsync(TempData.TaskItemId);
				if (TaskItem != null)
				{
					txtTitulo.Text = TaskItem.Title;
					txtDescripción.Text = TaskItem.Description;
					datePickerCreationDate.SelectedDate = TaskItem.CreationDate;
					cbCategory.SelectedValue = TaskItem.CategoryItem.Name;
					cbPriority.SelectedValue = TaskItem.PriorityItem.Name;
					cbEmployee.SelectedValue = TaskItem.User.Name;
					cbStatus.SelectedValue = TaskItem.StatusItem.Name;
				}
			}
			catch (Exception f)
			{
				MessageBox.Show(f.Message);
				this.Close();
			}
		}
		private async void UpdateTask_Click(object sender, RoutedEventArgs e)
		{
			bool isValid = await ValidateData();
			if(!isValid) {
				MessageBox.Show("Verifique la información");
			}
			else
			{
				// Get Category
				var Category = (from i in this.categoryItems
								where i.Name.Equals(cbCategory.Text)
								select i).FirstOrDefault();
				var priority = (from i in this.priorityItems
								where i.Name.Equals(cbPriority.Text)
								select i).FirstOrDefault();
				var idUser = (from i in this.usersDic
							  where i.Value== cbEmployee.Text
							  select i.Key).FirstOrDefault();
				var status = (from s in this.statusItems
							  where s.Name.Equals(cbStatus.Text)
							  select s
							  ).FirstOrDefault();
							  
				if(TempData.CurrentUser.IsAdmin)
				{
					TaskItem.CreationDate = datePickerCreationDate.SelectedDate.Value;
					TaskItem.UserId = idUser;
				}
				TaskItem.Title = txtTitulo.Text;
				TaskItem.Description = txtDescripción.Text;
				TaskItem.CategoryItemId = Category.CategoryItemId;
				TaskItem.CategoryItem = Category;
				TaskItem.PriorityItemId = priority.PriorityItemId;
				TaskItem.PriorityItem = priority;
				TaskItem.StatusItem = status;
				TaskItem.StatusItemId = status.StatusItemId;
				TaskItem.UserId = idUser;
				TaskItem.User =await B_User.GetUserAsync(idUser);

				bool Update = await B_Task.EditTaskItemAsync(TaskItem);
				if(Update)
				{
					MessageBox.Show("Tarea Actualizada");
					this.Close();
				}
				else
				{
					MessageBox.Show("Error");
					this.Close();
				}

			}
		}

		private async Task<bool> ValidateData()
		{
			try
			{
				if(string.IsNullOrEmpty(txtTitulo.Text) || string.IsNullOrEmpty(txtDescripción.Text))
				{
					return false;
				}
				else if(datePickerCreationDate.SelectedDate== null)
				{
					return false;
				}
				else if (string.IsNullOrEmpty(cbCategory.SelectedValue.ToString()))
				{
					return false;
				}
				else if (string.IsNullOrEmpty(cbPriority.SelectedValue.ToString()))
				{
					return false;
				}
				else if (string.IsNullOrEmpty(cbEmployee.SelectedValue.ToString()))
				{
					return false;
				}
				else if (string.IsNullOrEmpty(cbStatus.SelectedValue.ToString()))
				{
					return false;
				}
				else
				{
					return true;
				}
			}
			catch(Exception f)
			{
				MessageBox.Show(f.Message);
				return false;
			}
		}

		private void Cancel_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
