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
using Business;

namespace Presentation.Views
{
	/// <summary>
	/// Lógica de interacción para EditTime.xaml
	/// </summary>
	public partial class EditTime : Window
	{
		private TimeItem TimeItem { get; set; }
		public EditTime()
		{
			InitializeComponent();

            for (int i = 0; i <= 23; i++)
            {
                cbFinHour.Items.Add(i);
                cbInHour.Items.Add(i);
            }

for (int i = 0;i <= 59; i++)
            {
                cbInMinutes.Items.Add(i);
                cbFinMinutes.Items.Add(i);  

            }
            
          



		}

        private void LoadTime()
        {
            try
            {
                if (TempData.TimeItemId != 0)
                {
                    TimeItem =  B_Time.GetTimeItem(TempData.TimeItemId);

                    if (TimeItem != null)
                    {
                        cbInHour.SelectedValue = TimeItem.StartTime.Hour;
                        cbInMinutes.SelectedValue = TimeItem.StartTime.Minute;
                        cbFinHour.SelectedValue = TimeItem.EndTime.Hour;
                        cbFinMinutes.SelectedValue= TimeItem.EndTime.Minute;

                        txtNote.Text = TimeItem.Notes;
                        dpFecha.SelectedDate = TimeItem.StartTime.Date;

                    }
                }
            }
            catch (Exception f)
            { 

            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            
            var _time = dpFecha.SelectedDate.Value;
            var initialHour = (int)cbInHour.SelectedValue;
            var initialMinutes = (int)cbInMinutes.SelectedValue; // I assumed this should be cbInMinutes
            var finalHour = (int)cbFinHour.SelectedValue;
            var finalMinutes = (int)cbFinMinutes.SelectedValue;

            if(initialHour > finalHour)
            {
                MessageBox.Show("La hora Inicial no puede ser posterior a la hora final");
            }
            else if( (initialHour == finalHour) && ( initialMinutes > finalMinutes) )
            {
                MessageBox.Show("Los minutos en la hora inicial, no pueden ser mayores a los minutos en la hora final");
            }
            else if ((initialHour == finalHour) && (initialMinutes == finalMinutes))
            {
                MessageBox.Show("Las horas no pueden ser iguales");
            }
            else
            {

                // Constructing the initial time with the selected date and time from dropdowns
                DateTime IniTemp = new DateTime(_time.Year, _time.Month, _time.Day, initialHour, initialMinutes, 0); // Assuming seconds as 0

                // Constructing the final time using similar logic
                DateTime FinalTemp = new DateTime(_time.Year, _time.Month, _time.Day, finalHour, finalMinutes, 0); // Assuming seconds as 0

                TimeItem.EndTime = FinalTemp;
                TimeItem.StartTime = IniTemp;

                TimeItem.Notes = txtNote.Text;


                var d = B_Time.UpdateTime(TimeItem);
                if(d) {

                    MessageBox.Show("La información fue actualizada","Informacion",MessageBoxButton.OK,MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Error Interno", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }





        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadTime();

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
