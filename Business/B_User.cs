using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
	public static class B_User
	{
		public static async Task AddUserAsync()
		{
			try
			{
				throw new NotImplementedException();
			}
			catch(Exception ex)
			{
				Console.WriteLine($"Error AddUser: {ex.Message}");
			}
		}
		public static async Task GetUserAsync()
		{
			try
			{
				throw new NotImplementedException();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error GetUser: {ex.Message}");
			}
		}
		public static async Task ChangePasswordAsync()
		{
			try
			{
				throw new NotImplementedException();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error ChangePassword: {ex.Message}");
			}
		}
		public static async Task<bool> LogingAsync(int id,string password)
		{
			try
			{
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error GetUser: {ex.Message}");
				return false;
			}
		}
		public static async Task<List<User>> ListUsersAsync()
		{
			try
			{
				throw new NotImplementedException();
				return null;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error ListUser: {ex.Message}");
				return null;
			}
		}
		public static async Task<bool> IsAdminAsync()
		{
			try
			{
				throw new NotImplementedException();
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error GetUser: {ex.Message}");
				return false;
			}
		}
		public static async Task SetStatusAsync(int id,bool Status)
		{
			try
			{
				throw new NotImplementedException();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error GetUser: {ex.Message}");
			}
		}
	}
}
