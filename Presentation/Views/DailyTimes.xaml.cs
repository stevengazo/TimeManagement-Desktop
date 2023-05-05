using Microsoft.Win32;
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
using Excel = Microsoft.Office.Interop.Excel;

namespace Presentation.Views
{
	/// <summary>
	/// Lógica de interacción para DailyTimes.xaml
	/// </summary>
	public partial class DailyTimes : Window
	{
		private List<Task_Time_Report> reports = new List<Task_Time_Report>();
		public DailyTimes()
		{
			InitializeComponent();
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

		private async void SearchTask_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var SelectedDate = dtpDateSelected.SelectedDate;
				if (SelectedDate != null)
				{
					reports = await B_Export.ReportByDayWorkedAsync(SelectedDate, TempData.CurrentUser.UserId);
					if (reports.Count > 0 && reports != null)
					{
						reports = reports.OrderBy(R => R.StartTime).ToList();
						TimeSpan time = new();
                        foreach (var item in reports)
                        {
							time = time + (item.EndTime - item.StartTime);
                        }
						lblTiempo.Content = $"{time.Hours}:{time.Minutes} Registradas";
                        listViewTask.ItemsSource = reports;
					}
					else
					{
						MessageBox.Show("No hay coincidencias");
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void Clean_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				dtpDateSelected.SelectedDate = null;
				listViewTask.ItemsSource = null;
				lblTiempo.Content = string.Empty;

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private async void Export_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (reports.Count > 0)
				{
					SaveFileDialog saveFileDialog = new();
					saveFileDialog.Title = "Exportar Tareas";
					saveFileDialog.Filter = "Excel|*.xlsx";
					if (saveFileDialog.ShowDialog() == true)
					{
						string urlFile = saveFileDialog.FileName;
						var ExcelApp = new Excel.Application();
						ExcelApp.Workbooks.Add();

						var users = (from R in reports select R.UserName).Distinct().ToList();
						foreach (var item in users)
						{
							Excel._Worksheet _Worksheet = (Excel.Worksheet)ExcelApp.Worksheets.Add();
							_Worksheet.Name = $"Resumen de {item}";
							int counter = 2;
							#region Titulos
							_Worksheet.Cells[1, "A"] = nameof(Task_Time_Report.TaskItemId);
							_Worksheet.Cells[1, "b"] = nameof(Task_Time_Report.TimeItemId);
							_Worksheet.Cells[1, "c"] = nameof(Task_Time_Report.Title);
							_Worksheet.Cells[1, "d"] = nameof(Task_Time_Report.Description);
							_Worksheet.Cells[1, "e"] = nameof(Task_Time_Report.Priority);
							_Worksheet.Cells[1, "f"] = nameof(Task_Time_Report.Type);
							_Worksheet.Cells[1, "g"] = nameof(Task_Time_Report.UserName);
							_Worksheet.Cells[1, "h"] = nameof(Task_Time_Report.Hours);
							_Worksheet.Cells[1, "i"] = nameof(Task_Time_Report.Minutes);
							_Worksheet.Cells[1, "j"] = nameof(Task_Time_Report.StartTime);
							_Worksheet.Cells[1, "k"] = nameof(Task_Time_Report.EndTime);
							_Worksheet.Cells[1, "l"] = nameof(Task_Time_Report.notes);
							#endregion
							var taskpeerUser = (from R in reports where R.UserName == item select R).ToList();
							foreach (var report in reports)
							{
								_Worksheet.Cells[counter, "A"] = report.TaskItemId;
								_Worksheet.Cells[counter, "b"] = report.TimeItemId;
								_Worksheet.Cells[counter, "c"] = report.Title;
								_Worksheet.Cells[counter, "d"] = report.Description;
								_Worksheet.Cells[counter, "e"] = report.Priority;
								_Worksheet.Cells[counter, "f"] = report.Type;
								_Worksheet.Cells[counter, "g"] = report.UserName;
								_Worksheet.Cells[counter, "h"] = report.Hours;
								_Worksheet.Cells[counter, "i"] = report.Minutes;
								_Worksheet.Cells[counter, "j"] = report.StartTime.ToString("HH:mm");
								_Worksheet.Cells[counter, "k"] = report.EndTime.ToString("HH:mm");
								_Worksheet.Cells[counter, "l"] = report.notes;
								counter++;
							}
						}
						ExcelApp.ActiveWorkbook.SaveAs(urlFile, Excel.XlFileFormat.xlWorkbookDefault);
						ExcelApp.ActiveWorkbook.Close();
						ExcelApp.Quit();
						MessageBox.Show("Documento Generado", "Info");
					}
				}
				else
				{
					MessageBox.Show("No hay datos por exportar", "Advertencia");
				}

			}
			catch (Exception f)
			{
				MessageBox.Show(f.Message);
			}
		}

	}
}
