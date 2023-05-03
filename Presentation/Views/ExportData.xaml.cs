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

namespace Presentation.Views
{
	/// <summary>
	/// Lógica de interacción para ExportData.xaml
	/// </summary>
	public partial class ExportData : Window
	{
		Dictionary<int,string> months = new Dictionary<int, string>()
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
			catch (Exception ex) {
				MessageBox.Show(ex.Message); 
			}
            
		}

		public async void ExportData_click(object sender, RoutedEventArgs e)
		{
			try
			{
				SaveFileDialog saveFileDialog = new();
				saveFileDialog.Title = "Exportar Tareas";
				saveFileDialog.Filter = "Excel|*.xlsx";
				if(saveFileDialog.ShowDialog()== true)
				{
					string urlFile = saveFileDialog.FileName;
					var ExcelApp = new Excel.Application();
					ExcelApp.Workbooks.Add();
					Excel._Worksheet _Worksheet = (Excel.Worksheet)ExcelApp.ActiveSheet;
				}
			}
			catch(Exception f)
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
					foreach (Proyecto item in proyectos)
					{
						worksheet.Cells[contador, 1] = item.ProyectoId.ToString();
						worksheet.Cells[contador, 2] = item.Vendedor.Nombre;
						worksheet.Cells[contador, 3] = item.Cliente;
						worksheet.Cells[contador, 4] = item.FacturaAnticipoId;
						worksheet.Cells[contador, 5] = item.FacturaFinalId;
						worksheet.Cells[contador, 6] = item.PorcentajeAnticipo;
						worksheet.Cells[contador, 7] = item.TareaId;
						worksheet.Cells[contador, 8] = item.FechaOC.ToLongDateString();
						worksheet.Cells[contador, 9] = item.OfertaId.ToString();
						worksheet.Cells[contador, 10] = item.FechaInicio.ToLongDateString();
						worksheet.Cells[contador, 11] = item.FechaFinal.ToLongDateString();
						worksheet.Cells[contador, 12] = item.Monto.ToString("C", CultureInfo.CurrentCulture);
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
				if( string.IsNullOrEmpty( cbUsers.Text) && string.IsNullOrEmpty(cbMonth.Text))
				{
					MessageBox.Show("No se puede procesar la busqueda, seleccione los dos elementos", "Información", MessageBoxButton.OK);
				}
				else
				{
					int user = (from U in usersDic where U.Value == cbUsers.Text select U.Key).FirstOrDefault();
					int monthId = (from M in months where M.Value == cbMonth.Text select M.Key).FirstOrDefault();
					var Results = await B_Export.GenerateReportAsync(user, monthId);
					if (Results.Count > 0)
					{
						listView.ItemsSource = Results;
					}
				}
				
			}catch(Exception de)
			{
				MessageBox.Show(de.Message);
			}
		}

	
	}
}
