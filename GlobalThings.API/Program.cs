using GlobalThings.API.Extentions;
using GlobalThings.Domain.Configuration;
using GlobalThings.Domain.Jobs;
using Hangfire;
using Hangfire.MemoryStorage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<MonitorConfiguration>(builder.Configuration);

builder.Services.AddHangfire(op =>
{
    op.UseMemoryStorage();
});
builder.Services.AddHangfireServer();

builder.Services.AddNativeIoC(builder.Configuration);


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseHangfireDashboard();

RecurringJob.AddOrUpdate<SensorMonitoringJob>(nameof(SensorMonitoringJob), x => x.MonitorSensorsAsync(), "*/5 * * * *");

app.Run();

