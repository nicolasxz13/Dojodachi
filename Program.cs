var builder = WebApplication.CreateBuilder(args);

// Add our service
builder.Services.AddControllersWithViews();
#region session service.
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
#endregion
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

#region Session
app.UseSession();
#endregion
app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
