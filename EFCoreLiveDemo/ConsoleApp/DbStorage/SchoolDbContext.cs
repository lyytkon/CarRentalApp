using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ConsoleApp.Models;
using System.Reflection;

namespace ConsoleApp.DbStorage
{
	public class SchoolDbContext : DbContext
	{
		// Tell EFCore about the model casses
		public DbSet<Student> Student => Set<Student>();
		public DbSet<Education> Education => Set<Education>();


		// Make our own version of OnConfiguring
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			string? exeFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			optionsBuilder.UseSqlite($@"Data source = {exeFolder}\School.db");
		}
	}
}
