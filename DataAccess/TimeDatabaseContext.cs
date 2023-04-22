using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models;
using System.Configuration;

namespace DataAccess
{
	public class TimeDatabaseContext: DbContext
	{
		#region  Internal Attributes
		internal string StringConnection { get; set; }
		internal IConfiguration Configuracion { get; set; }
		#endregion
		public DbSet<CategoryItem> CategoryItems { get; set; }
		public DbSet<PriorityItem> PriorityItems { get; set; }
		public DbSet<StatusItem> StatusItems { get; set; }	
		public DbSet<TaskItem> TaskItems { get; set; }
		public DbSet<TimeItem> TimeItems { get; set; }	
		public DbSet<User> Users { get; set; }
		private void GetConnectionString(string connectionStringName = "DBTasks")
		{
			StringConnection = XMLConfigurations.LeerCadenaDeConexion();
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			// You don't actually ever need to call this
			if (!optionsBuilder.IsConfigured)
			{
				GetConnectionString();
				optionsBuilder.UseSqlServer(StringConnection);
			}
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);			
		}
	}
}
