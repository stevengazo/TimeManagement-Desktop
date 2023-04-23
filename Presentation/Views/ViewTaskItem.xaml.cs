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
using Business;
using Models;

namespace Presentation.Views
{
	/// <summary>
	/// Interaction logic for ViewTaskItem.xaml
	/// </summary>
	public partial class ViewTaskItem : Window
	{
		public  TaskItem viewTaskItem { get; set; }
		
		public ViewTaskItem()
		{
			InitializeComponent();
			LoadTask();
			LoadCombobox();
		}

		private void LoadCombobox()
		{
			for (int i = 0; i <= 23; i++)
			{
				cbInHour.Items.Add(i.ToString());
				cbFinHour.Items.Add(i.ToString());
			}
			for (int i = 0; i <= 59; i++)
			{
				cbInMinutes.Items.Add(i.ToString());
				cbFinMinutes.Items.Add(i.ToString());
			}			
		}

		private async void LoadTask()
		{
			if (TempData.TaskItemId > 0)
			{
				viewTaskItem = await B_Task.GetTaskItemAsync(TempData.TaskItemId);

				lblTitle.Content = viewTaskItem.Title;
				lblPriority.Content = viewTaskItem.PriorityItem.Name;
				lblStatus.Content = viewTaskItem.StatusItem.Name;
				lblDate.Content = viewTaskItem.CreationDate.ToString("dd-MMM-yyyy");
				listViewTimeItems.ItemsSource = viewTaskItem.TimeItems;
			}
			else
			{
				MessageBox.Show("Error Id de tarea no es valido");
				this.Close();
			}
		}
	}
}
