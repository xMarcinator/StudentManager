using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StudentManager.Models.DBUtils;
using StudentManager.Utils;

namespace StudentManager.Models;

public class StudentManagementDb : DbContext
{
    public StudentManagementDb(DbContextOptions<StudentManagementDb> options) : base(options)
    {
    }

    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<ClassModel> Classes { get; set; } = null!;
    public DbSet<Course> Courses { get; set; } = null!;
    public DbSet<Education> Educations { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string? connectionString = Environment.GetEnvironmentVariable("DBSTRING");

        if (connectionString is null)
        {
            throw new Exception("DBSTRING environment variable not set");
        }

        SqlConnectionStringBuilder builder = new(connectionString);

        //Console.WriteLine(builder.InitialCatalog);

        if (Environment.GetEnvironmentVariable("DBNAME").IsNotNull(out var DatabaseName))
            builder.InitialCatalog = DatabaseName;

        if (Environment.GetEnvironmentVariable("DBUSER").IsNotNull(out var user))
            builder.UserID = user;

        if (Environment.GetEnvironmentVariable("DBPASS").IsNotNull(out var pass))
            builder.Password = pass;
        
        Console.WriteLine(builder.ConnectionString);
        
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

        //explicit naming override to give better table names
        modelBuilder.Entity<ClassModel>()
            .HasMany(e => e.ClassCourses)
            .WithMany(e => e.Classes)
            .UsingEntity("ClassAssignedCourses");

        modelBuilder.Entity<Student>()
            .HasMany(e => e.ExplicitCourses)
            .WithMany(e => e.ExplicitStudents)
            .UsingEntity("StudentAssignedCourses");
    }
}