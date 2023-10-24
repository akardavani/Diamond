using Diamond.Api.Infrastructure;
using Diamond.Domain.Models.Identity;
using Diamond.Domain.Setting;
using Diamond.Infrastructure;
using Diamond.Jobs;
using Diamond.Services;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

//Add Configure
ConfigureServices(builder.Services);

builder.Services.AddDiamondServices();

//builder.Services.AddDiamondInfrastructure();
//builder.Services.AddHostedService<JobSchedule>();
//builder.Services.AddHostedService<HostedServiceManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();



void ConfigureServices(IServiceCollection services)
{
    //services.AddSingleton<IManagerStoreBusiness, ManagerStoreBusiness>();
    services.AddDbContext<DiamondDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DiamondDbContext")));
    services.AddIdentity<User, Role, DiamondDbContext>(builder.Configuration);
    services.Configure<Settings>(
        builder.Configuration.GetSection("Settings"));
    services.AddDiamondInfrastructure();
    services.AddMessageManager(builder.Configuration);
    services.AddHostedService<JobSchedule>();
    services.AddHostedService<HostedServiceManager>();
}