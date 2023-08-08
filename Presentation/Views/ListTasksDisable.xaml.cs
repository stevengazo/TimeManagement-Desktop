using Business;
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
	/// Lógica de interacción para ListTasksDisable.xaml
	/// </summary>
	public partial class ListTasksDisable : Window
	{
		public ListTasksDisable()
		{
			InitializeComponent();
			LoadTasks();
		}
		private async Task LoadTasks()
		{
			try
			{
				listViewTaskItems.ItemsSource = await B_Task.ListTaskItemsAsync(TempData.CurrentUser.UserId,true);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		#region ApplicationCOmmands
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
		private async void CanExecuteDeleteTask(object target, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}
		private async void CanExecuteEditTask(object target, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}
		private async void EditTaskItem(object target, ExecutedRoutedEventArgs args)
		{
			try
			{
				var numberOfTask = int.Parse(args.Parameter.ToString());
				TempData.TaskItemId = numberOfTask;
				EditTask editTask = new EditTask();
				editTask.ShowDialog();
			}
			catch (Exception f)
			{
				MessageBox.Show(f.Message);
			}
		}
		private async void EnableTaskItem(object target, ExecutedRoutedEventArgs args)
		{
			try
			{
				var numberOfTask = int.Parse(args.Parameter.ToString());
				bool isDisable = await B_Task.EnableTask(numberOfTask);
				if (isDisable)
				{
					MessageBox.Show($"La tarea fue activada de nuevo", "Informacion",  MessageBoxButton.OK, MessageBoxImage.Information);
					await LoadTasks();
				}
			}
			catch (Exception f)
			{
				MessageBox.Show(f.Message);
			}
		}
        #endregion

        private async void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
			if (string.IsNullOrEmpty(txtBuscador.Text))
			{
				MessageBox.Show("No hay informaciion para buscar", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);

			}
			else
			{
				var list = B_Task.SearchTaskByTitle(txtBuscador.Text);
				if (list.Count == 0)
				{
                    MessageBox.Show("No hay coincidencias", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
				}
				else
				{
					listViewTaskItems.ItemsSource = list;
				}
			}
        }

        private async void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            listViewTaskItems.ItemsSource = await B_Task.ListTaskItemsAsync(TempData.CurrentUser.UserId, true);
			txtBuscador.Text = string.Empty;

        }
    }
}
