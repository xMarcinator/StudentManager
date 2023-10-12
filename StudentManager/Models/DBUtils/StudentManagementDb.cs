using System.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StudentManager.Models.DBUtils;
using StudentManager.Utils;

namespace StudentManager.Models;

public class StudentManagementDb : DbContext
{
    public StudentManagementDb(DbContextOptions<StudentManagementDb> options) : base(options) { }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string? connectionString = Environment.GetEnvironmentVariable("DBSTRING");

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
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClassModel>(builder =>
        {
            // Date is a DateOnly property and date on database
            builder.Property(x => x.StartDate)
                .HasConversion<DateOnlyConverter, DateOnlyComparer>();
        });
    }

    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<ClassModel> Classes { get; set; } = null!;
    public DbSet<Course> Courses { get; set; } = null!;
    public DbSet<Education> Educations { get; set; } = null!;
}

