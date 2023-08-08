using System;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Security.Permissions;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Linq;
using Business;
using Models;

namespace Presentation
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class Login : Window
	{
		public Login()
		{
			InitializeComponent();
			LoadCredentialsAsync();
		}

		private async void login()
		{
            try
            {
                var id = txtUsuario.Text;
                var pass = txtPassword.Password;
                int idUsuario = int.Parse(id);
                bool isValid = B_User.LogingAsync(idUsuario, pass).Result;
                if (isValid)
                {
                    TempData.CurrentUser = await B_User.GetUserAsync(idUsuario);
                    TempData.idUser = idUsuario;
                    var viewHome = new Home();
                    // Save values
                    bool RememberPass = ckBPassword.IsChecked.Value;
                    if (RememberPass)
                    {
                        await SaveCredentialsAsync(idUsuario.ToString(), pass);
                    }
                    this.Hide();
                    viewHome.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Datos Invalidos");
                }
            }
            catch (Exception ef)
            {
                MessageBox.Show($"Error: {ef.Message}");
            }
        }
		private async void OnLoging(object sender, RoutedEventArgs e)
		{
			login();	
		}
		private async Task SaveCredentialsAsync(string user, string pass)
		{
			try
			{
				string urlArchivo = Path.Combine(Directory.GetCurrentDirectory(), "temporals.xml");
				if (File.Exists(urlArchivo))
				{
					File.Delete(urlArchivo);
				}
				XDocument document = new XDocument(
													new XDeclaration("1.0", "utf-8", "yes"),
													new XComment("XML from plain code"),

													new XElement("root",
														new XElement(nameof(User.UserId), user),
														new XElement(nameof(User.Password), pass)
														));			
				document.Save(urlArchivo);
			}
			catch(Exception f)
			{
				MessageBox.Show(f.Message,"Error SaveCredentias");
			}			
		}
		private async Task LoadCredentialsAsync()
		{
			try
			{
				string urlArchivo = Path.Combine(Directory.GetCurrentDirectory(), "temporals.xml");
				if (File.Exists(urlArchivo))
				{
					var data = new FileStream(urlArchivo, FileMode.Open, FileAccess.Read);
					var documento = new XmlDataDocument();
					documento.Load(data);
					XmlNodeList nodeList = documento.GetElementsByTagName("root");
					string userId = string.Empty;
					string password = string.Empty;
					foreach (XmlNode item in nodeList)
					{
						userId = item.SelectSingleNode(nameof(User.UserId)).InnerText.ToString();
						password = item.SelectSingleNode(nameof(User.Password)).InnerText.ToString();
					}
					txtPassword.Password = password;
					txtUsuario.Text = userId;
					ckBPassword.IsChecked = true;
				}
			}catch(Exception f)
			{
				MessageBox.Show(f.Message);
			}
		}

        private async void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
			if (e.Key == System.Windows.Input.Key.Enter)
			{
				login();
			}

        }

        private async void Window_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
			if(e.Key == System.Windows.Input.Key.Enter)
			{
                login();
            }
        }
    }
}
