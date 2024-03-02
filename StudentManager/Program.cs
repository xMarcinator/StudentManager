using Microsoft.AspNetCore.Identity;
using StudentManager.Authentication;
using StudentManager.Models;
using StudentManager.Models.DBUtils;
using StudentManager.Models.Json;
using StudentManager.Utils.LoginHelpers;
using StudentManager.Utils.LoginHelpers.Providers;

DotNetEnv.Env.TraversePath().Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()    
    .AddRazorOptions(options =>
{
    options.ViewLocationFormats.Add("/{0}.cshtml");
});;

builder.Services.AddDbContext<StudentManagementDb>();

builder.Services.AddScoped<IModelRepository<Student>, EFStudentRepository>();
builder.Services.AddScoped<IModelRepository<Education>, EFEducationRepository>();
builder.Services.AddScoped<IModelRepository<ClassModel>, EFClassRepository>();
builder.Services.AddScoped<IModelRepository<Course>, EFCourseRepository>();

builder.Services.AddSingleton<OAuthProviderService, OAuthProviderService>();

builder.Services.AddDbContext<AppIdentityDbContext>();
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppIdentityDbContext>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("TrialOnly", policy =>
    {
        policy.RequireClaim("Trial");
    });
    options.AddPolicy ("AdminOnly", policy => {
        policy.RequireRole ("Admin");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


SeedDb.EnsurePopulated(app);
IdentitySeedData.EnsurePopulated(app);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


//app.UseAuthorization();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Student}/{action=List}");

/*Type t = typeof(MicrosoftOAuthProviderCopy);
Type t2 = typeof(BaseOAuthProvider);

BaseOAuthProvider hell0 = (BaseOAuthProvider) new MicrosoftOAuthProviderCopy();*/

//Console.WriteLine(t2.IsAssignableFrom(t));


app.Run();

public partial class Program
{
}