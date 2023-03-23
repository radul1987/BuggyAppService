using System.Net;

var builder = WebApplication.CreateBuilder(args);
var builder2 = WebApplication.CreateBuilder();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder2.Services.AddControllersWithViews();

var app = builder.Build();
var app2 = builder2.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

string startupIssue = "false";
try
{
     startupIssue = System.Environment.GetEnvironmentVariables()["APPSETTING_StartupIssue"].ToString();

}
catch(Exception ex)
{
   
}
// startupIssue = app.Configuration.GetValue<string>(("APPSETTING_StartupIssue"));

if (startupIssue == "true")
{
    throw new Exception("startup issue");
}
else if (startupIssue == "coldStart60")
{
    System.Threading.Thread.Sleep(60000);

}
else if (startupIssue == "coldStart240")
{
    System.Threading.Thread.Sleep(240000);

}




app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();



app2.UseHttpsRedirection();
app2.UseStaticFiles();

app2.UseRouting();

app2.UseAuthorization();

app2.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app2.Run();




