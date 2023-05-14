using Lab16.Models;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IPersonRepository, FakePersonRepository>(); 

var app = builder.Build();
app.UseStaticFiles();
app.UseDeveloperExceptionPage();
app.UseStatusCodePages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=AddPerson}/{id?}"
);

app.Run();
