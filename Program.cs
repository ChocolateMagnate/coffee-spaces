using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using spider_web.Data;



var builder = WebApplication.CreateBuilder(args);
//string[] origins = new string[3] { "http://localhost:4200", "http://localhost:7093", "http://localhost:5226" };


builder.Services.AddCors(options =>
{
    options.AddPolicy("Allow-local-host", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
                      });
});


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMvc();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseCors();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
