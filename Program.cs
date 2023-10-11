using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

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
    startupIssue = System.Environment.GetEnvironmentVariables()["StartupIssue"].ToString();

}
catch (Exception ex)
{

}
// startupIssue = app.Configuration.GetValue<string>(("APPSETTING_StartupIssue"));

if (startupIssue.ToLower() == "true")
{
    throw new Exception("startup issue");
}

else if (int.TryParse(startupIssue, out int coldsStartValue))
{
    System.Threading.Thread.Sleep(coldsStartValue * 1000);
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




