using System.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StudentManager.Utils;

namespace StudentManager.Models;

public class StudentManagementDb : DbContext
{
    public StudentManagementDb(DbContextOptions<StudentManagementDb> options) : base(options) { }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string? connectionString = System.Environment.GetEnvironmentVariable("DBSTRING");

        if (connectionString is null)
        {
            throw new Exception("DBSTRING environment variable not set");
        }
        
        SqlConnectionStringBuilder builder = new(connectionString);
        
        Console.WriteLine(builder.InitialCatalog);
        
        builder.InitialCatalog = Environment.GetEnvironmentVariable("DBNAME") 
                                 ?? throw new Exception("DBNAME environment variable not set");
        
        if (Environment.GetEnvironmentVariable("DBUSER").IsNotNull(out var user))
            builder.UserID = user;
        
        if (Environment.GetEnvironmentVariable("DBPASS").IsNotNull(out var pass))
            builder.Password = pass;
        
        optionsBuilder.UseSqlServer(builder.ConnectionString);
    }

    public DbSet<Student> Students { get; set; } = null!;
}

