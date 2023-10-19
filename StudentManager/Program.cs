using StudentManager.Models;
using StudentManager.Models.DBUtils;

DotNetEnv.Env.TraversePath().Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<StudentManagementDb>();

builder.Services.AddScoped<IModelRepository<Student>, EFStudentRepository>();
builder.Services.AddScoped<IModelRepository<Education>, EFEducationRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


SeedDb.EnsurePopulated(app);

Console.WriteLine("populated database");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


//app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Student}/{action=List}");

app.Run();

public partial class Program
{
}