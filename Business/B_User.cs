using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Business
{
	public static class B_User
	{
		public static async Task<bool> AddUserAsync(User user)
		{
			try
			{
				if(user.UserId >= 0) {

					user.IsArchive = false;
					user.IsEnable = false;
					user.IsAdmin = false;
					// Hashing Password

					using (TimeDatabaseContext db = new())
					{
						db.Users.Add(user);
						await db.SaveChangesAsync();
						return true;
					}
				}
				else
				{
					return false;
				}
			}
			catch(Exception ex)
			{
				Console.WriteLine($"Error AddUser: {ex.Message}");
				return false;
			}
		}
		public static async Task<User> GetUserAsync(int UserId)
		{
			try
			{
				User usertmp = new();
				using (TimeDatabaseContext db = new()) {
					usertmp = await (from i in db.Users where i.UserId == UserId select i).FirstOrDefaultAsync();
					return usertmp;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error GetUser: {ex.Message}");
				return null;
			}
		}
		public static async Task<bool> ChangePasswordAsync(int UserId, string Password)
		{
			try
			{
				using( TimeDatabaseContext db = new())
				{
					User user = new();
					user = await (from U in db.Users where U.UserId == UserId select U).FirstOrDefaultAsync();
					if(user != null)
					{
						// Hass Password	
					}
					db.Users.Update(user);
					await db.SaveChangesAsync();
					return true;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error ChangePassword: {ex.Message}");
				return false;
			}
		}
		public static async Task<bool> LogingAsync(int id,string password)
		{
			try
			{
				using( TimeDatabaseContext db = new())
				{
					int count = 0;
					count = (from U in db.Users where U.UserId == id && U.Password == password select U).Count();
					if(count> 0)
					{
						return true;
					}
					else
					{
						return false;
					}
				}
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
				var ListUsers = new List<User>();
				using (TimeDatabaseContext bd = new())
				{
					ListUsers = bd.Users.ToList();
					foreach (var item in ListUsers)
					{
						item.Password = string.Empty;
					}
				}
				return ListUsers;
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
		public static async Task<bool> ExistsId(int idToSearch)
		{
			try
			{
				using (TimeDatabaseContext db = new())
				{
					var idSearched = (	from i in db.Users
										where i.UserId == idToSearch
										select i).Count();
					if(idSearched> 0)
					{
						return true;
					}
					else
					{
						return false;
					}
					
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error GetUser: {ex.Message}");
				return true;
			}

		}



		public static async Task<int> GetLastUserIdAsync()
		{
			try
			{
				using (TimeDatabaseContext db = new())
				{
					var lastId = await (from i in db.Users
								  orderby i.UserId descending
								  select i.UserId).FirstOrDefaultAsync();
					return lastId;
				}
			}catch(Exception ex)
			{
				Console.WriteLine($"Error GetUser: {ex.Message}");
				return -5;
			}
		}
	}
}
