using Core;
using Core.Interfaces;
using Identity;
using Identity.Models;
using Identity.Seeds;
using Infraestructure;
using Microsoft.AspNetCore.Identity;
using WebApi;
using WebApi.Extension;

IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build();


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApiVersioningExtension();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAplicationLayer();
builder.Services.AddIdentityInfra(configuration);
builder.Services.AddSharedInfraestruture(configuration);
builder.Services.AddPersintenceInfraestructure(configuration);


var app = builder.Build();
{
    using (var sco = app.Services.CreateScope())
    {
        var serv = sco.ServiceProvider;
        try
        {
            var userManager = serv.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serv.GetRequiredService<RoleManager<IdentityRole>>();

            await DefaultRoles.SeedAsync(userManager, roleManager);
            await DefaultAdminUser.SeedAsync(userManager, roleManager);
            await DefaultBasicUser.SeedAsync(userManager, roleManager);
        }
        catch (Exception ex)
        {

            throw;
        }
    }
}


//app.Run();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseErrorHandlerMiddelware();
app.MapControllers();
app.Run();
