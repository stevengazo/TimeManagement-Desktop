using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
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
using Microsoft.Win32;
using System.Globalization;
using System.DirectoryServices.ActiveDirectory;
using System.Diagnostics.Metrics;

namespace Presentation.Views
{
	/// <summary>
	/// Lógica de interacción para ExportData.xaml
	/// </summary>
	public partial class ExportData : Window
	{
		private List<TaskItemReport> reports = new List<TaskItemReport>();
		Dictionary<int, string> months = new Dictionary<int, string>()
		{
			{1, "Enero" },
			{2, "Febrero" },
			{3, "Marzo" },
			{4, "Abril" },
			{5, "Mayo" },
			{6, "Junio" },
			{7, "Julio" },
			{8, "Agosto" },
			{9, "Septiembre" },
			{10, "Octubre" },
			{11, "Noviembre" },
			{12, "Diciembre" }
		};
		Dictionary<int, string> usersDic = new Dictionary<int, string>();

		public ExportData()
		{
			InitializeComponent();
			LoadData();
		}
		private async Task LoadData()
		{
			try
			{
				foreach (var item in months)
				{
					cbMonth.Items.Add(item.Value);
				}
				usersDic = await B_User.DicUsersAsync();
				foreach (var item in usersDic)
				{
					cbUsers.Items.Add(item.Value);
				}
				cbUsers.Items.Add("Todos");
				this.Show();

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

		}

		public async void ExportData_click(object sender, RoutedEventArgs e)
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
						var listOfUsers = (from U in reports select U.User).Distinct().ToList();
						foreach (var UserName in listOfUsers)
						{
							ExcelApp.Workbooks.Add();
							Excel._Worksheet _Worksheet = (Excel.Worksheet)ExcelApp.ActiveSheet;
							_Worksheet.Cells[1, "A"] = "Numero Proyecto";
							_Worksheet.Name = $"{UserName}";
							var resportPeerUser = reports.Where(R => R.User.Equals(UserName)).ToList();
							

							#region Titulos
							_Worksheet.Cells[1, "A"] = nameof(TaskItemReport.User);
							_Worksheet.Cells[1, "b"] = nameof(TaskItemReport.Title);
							_Worksheet.Cells[1, "c"] = nameof(TaskItemReport.Description);
							_Worksheet.Cells[1, "D"] = nameof(TaskItemReport.Created);
							_Worksheet.Cells[1, "e"] = nameof(TaskItemReport.Category);
							_Worksheet.Cells[1, "f"] = nameof(TaskItemReport.Status);
							_Worksheet.Cells[1, "g"] = nameof(TaskItemReport.Id);
							_Worksheet.Cells[1, "h"] = nameof(TimeItem.TaskItemId);
							_Worksheet.Cells[1, "i"] = nameof(TimeItem.StartTime);
							_Worksheet.Cells[1, "j"] = nameof(TimeItem.EndTime);
							_Worksheet.Cells[1, "k"] = "Duration";
							_Worksheet.Cells[1, "l"] = nameof(TimeItem.Notes);

							#endregion
							int counter = 2;

							foreach (var ReportItem in resportPeerUser)
							{
								_Worksheet.Cells[counter, "A"] = ReportItem.User;
								_Worksheet.Cells[counter, "B"] = ReportItem.Title;
								_Worksheet.Cells[counter, "C"] = ReportItem.Description;
								_Worksheet.Cells[counter, "D"] = ReportItem.Created.Value.ToString("dd-MM-yyyy");
								_Worksheet.Cells[counter, "E"] = ReportItem.Category;
								_Worksheet.Cells[counter, "F"] = ReportItem.Status;
								_Worksheet.Cells[counter, "G"] = ReportItem.Id;
								if (ReportItem.timeItems.Count > 0)
								{
									foreach (var timeitem in ReportItem.timeItems)
									{
										_Worksheet.Cells[counter, "H"] = ReportItem.Id.ToString();
										_Worksheet.Cells[counter, "I"] = timeitem.StartTime.ToString("dd-MM-yy HH:mm");
										_Worksheet.Cells[counter, "J"] = timeitem.EndTime.ToString("dd-MM-yy HH:mm");
										_Worksheet.Cells[counter, "K"] = (timeitem.EndTime - timeitem.StartTime).ToString();
										_Worksheet.Cells[counter, "L"] = timeitem.Notes;
			
										counter++;
									}
								}
								else
								{
									counter++;
								}
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


		/*	private void excelToolStripMenuItem_Click(object sender, EventArgs e)
			{
				try
				{
				//	saveFileDialog1.Title = "Exportar a Excel";
				//	saveFileDialog1.Filter = "Excel|*.xlsx";
				//	if (saveFileDialog1.ShowDialog() == DialogResult.OK)
					{
						string URLArchivo = saveFileDialog1.FileName;
						var ExcelApp = new Excel.Application();
						ExcelApp.Workbooks.Add();
						Excel._Worksheet worksheet = (Excel.Worksheet)ExcelApp.ActiveSheet;
						worksheet.Cells[1, "A"] = "Numero Proyecto";

						int contador = 2;
						foreach (Proyecto UserName in proyectos)
						{
							worksheet.Cells[contador, 1] = UserName.ProyectoId.ToString();
							worksheet.Cells[contador, 2] = UserName.Vendedor.Nombre;
							worksheet.Cells[contador, 3] = UserName.Cliente;
							worksheet.Cells[contador, 4] = UserName.FacturaAnticipoId;
							worksheet.Cells[contador, 5] = UserName.FacturaFinalId;
							worksheet.Cells[contador, 6] = UserName.PorcentajeAnticipo;
							worksheet.Cells[contador, 7] = UserName.TareaId;
							worksheet.Cells[contador, 8] = UserName.FechaOC.ToLongDateString();
							worksheet.Cells[contador, 9] = UserName.OfertaId.ToString();
							worksheet.Cells[contador, 10] = UserName.FechaInicio.ToLongDateString();
							worksheet.Cells[contador, 11] = UserName.FechaFinal.ToLongDateString();
							worksheet.Cells[contador, 12] = UserName.Monto.ToString("C", CultureInfo.CurrentCulture);
							contador++;
						}
						ExcelApp.ActiveWorkbook.SaveAs(URLArchivo, Excel.XlFileFormat.xlWorkbookDefault);
						ExcelApp.ActiveWorkbook.Close();
						ExcelApp.Quit();
						MessageBox.Show("Documento Generado", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Ocurrió un problema. Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}*/

		private async void Search_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (string.IsNullOrEmpty(cbUsers.Text) && string.IsNullOrEmpty(cbMonth.Text))
				{
					MessageBox.Show("No se puede procesar la busqueda, seleccione los dos elementos", "Información", MessageBoxButton.OK);
				}
				else
				{
					int user = (from U in usersDic where U.Value == cbUsers.Text select U.Key).FirstOrDefault();
					int monthId = (from M in months where M.Value == cbMonth.Text select M.Key).FirstOrDefault();
					reports = await B_Export.GenerateReportAsync(user, monthId);
					if (reports.Count > 0)
					{
						listView.ItemsSource = reports;
					}
				}

			}
			catch (Exception de)
			{
				MessageBox.Show(de.Message);
			}
		}


	}
}
