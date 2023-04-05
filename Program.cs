using TestTaskDotnet.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddServices();
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3001",
                                              "http://localhost:3001/register",
                                              "http://localhost:3001/create",
                                              "http://localhost:3001/requests",
                                              "http://localhost:3001/history",
                                              "http://localhost:3000",
                                              "http://localhost:3000/register",
                                              "http://localhost:3000/create",
                                              "http://localhost:3000/requests",
                                              "http://localhost:3000/history")
                            .WithMethods("GET,POST,PUT,DELETE")
                            .AllowAnyHeader();

                      });
});

var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
