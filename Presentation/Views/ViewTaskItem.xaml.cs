using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Microsoft.IdentityModel.Logging;
using Models;

namespace Presentation.Views
{
	/// <summary>
	/// Interaction logic for ViewTaskItem.xaml
	/// </summary>
	public partial class ViewTaskItem : Window
	{
		public  TaskItem viewTaskItem { get; set; }
		public  float _Minutes { get; set; }
		
		public ViewTaskItem()
		{
			InitializeComponent();
			LoadTask();
			LoadCombobox();
		}



		private async void SearchDate_Click(object sender, RoutedEventArgs e)
		{
			try
			{
			 var Date = dtpSearchDate.SelectedDate;
				if(Date!= null)
				{
					DateTime tmp = Date.Value;
					List<TimeItem> TimeItemsList = new();
					TimeItemsList = await B_Time.ListTimeItemsAsync(tmp, viewTaskItem.TaskItemId);
					if(TimeItemsList.Count > 0)
					{					
						listViewTimeItems.ItemsSource = TimeItemsList;
					}
					else
					{
						MessageBox.Show("No hay coindencias");
					}
				}
			}catch(Exception f)
			{
				MessageBox.Show(f.Message);
			}
		}
		private async void CleanSearch_Click(object sender, RoutedEventArgs e)
		{
			//dtpSearchDate.SelectedDate = DateTime.Today;
			dtpSearchDate.SelectedDate = null;
			LoadTask();
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
				await loadTimes();				
			}
			else
			{
				MessageBox.Show("Error Id de tarea no es valido");
				this.Close();
			}
		}

		private async Task loadTimes()
		{
			if(viewTaskItem.TimeItems != null)
			{
				_Minutes = 0;
				List<TimeItemWithQuantity> listTimes = new() ;
				foreach (var item in viewTaskItem.TimeItems)
				{

					TimeSpan TimeWorked = (item.EndTime - item.StartTime);
					var Hours = TimeWorked.Hours;
					var totalMinutes = Convert.ToInt16(TimeWorked.TotalMinutes);
					var Minutes = TimeWorked.Minutes - (TimeWorked.Minutes *60);


					TimeItemWithQuantity i = new() {
						TimeItemId = item.TimeItemId,
						Hours = Hours,
						Minutes = Minutes,
						StartTime = item.StartTime,
						EndTime = item.EndTime						
					};
					_Minutes = _Minutes + (i.Hours * 60);
					listTimes.Add(i);
				}
				lblHoras.Content = $"{_Minutes / 60} Horas";
				listViewTimeItems.ItemsSource = listTimes;
			}
        }

		private async Task CleanInput()
		{
			cbFinHour.SelectedIndex = -1;
			cbFinMinutes.SelectedIndex = -1;
			cbInHour.SelectedIndex = -1;
			cbInMinutes.SelectedIndex = -1;
			txtNotes.Text = string.Empty;
		}

		private async void Limpiar_Click(object sender, RoutedEventArgs e)
		{
			await CleanInput();
		}

		private async void AddTime_Click(object sender, RoutedEventArgs e)
		{
			var initialHour = int.Parse( cbInHour.Text);
			var initialMinutes = int.Parse(cbInMinutes.Text);
			var finishHour = int.Parse(cbFinHour.Text);
			var finishMinutes = int.Parse(cbFinMinutes.Text);

			var tmpTimeMinutes = (initialHour * 60) + initialMinutes;
			var tmptimeFinalMinutes = (finishHour * 60) + finishMinutes;
			if(tmptimeFinalMinutes < tmpTimeMinutes)
			{
				MessageBox.Show("La hora inicial no puede ser posterior a la hora final");
				await CleanInput();
			}
			else
			{
				TimeSpan tpInit = new TimeSpan(initialHour, initialMinutes, 0);
				TimeSpan tpFinish = new TimeSpan(finishHour, finishMinutes, 0);
				TimeItem timeItem = new TimeItem() {
					EndTime = DateTime.Today + tpFinish,
					StartTime = DateTime.Today + tpInit,
					Notes = txtNotes.Text,
					TaskItemId = viewTaskItem.TaskItemId
				};
				var Status =await  B_Time.AddTime(timeItem);
				if (Status)
				{
					await CleanInput();
					LoadTask();
				}			
			}
		}
    }
}
