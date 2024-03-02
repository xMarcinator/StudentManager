using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StudentManager.Utils;

namespace StudentManager.Models.DBUtils;

public class AppIdentityDbContext : IdentityDbContext<IdentityUser> {
    public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
        : base(options) { }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        Console.WriteLine("Configuring db");
        string connectionString = Environment.GetEnvironmentVariable("DBSTRING") 
                                   ?? throw new Exception("DBSTRING environment variable not set") ;

        SqlConnectionStringBuilder builder = new(connectionString);

        //Console.WriteLine(builder.InitialCatalog);

        if (Environment.GetEnvironmentVariable("IDENTITYDBNAME").IsNotNull(out var DatabaseName))
            builder.InitialCatalog = DatabaseName;

        if (Environment.GetEnvironmentVariable("DBUSER").IsNotNull(out var user))
            builder.UserID = user;

        if (Environment.GetEnvironmentVariable("DBPASS").IsNotNull(out var pass))
            builder.Password = pass;

        Console.WriteLine(builder.ConnectionString);
        
        optionsBuilder.UseSqlServer(builder.ConnectionString);
    }
}