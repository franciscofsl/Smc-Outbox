using SmcOutbox.Application;
using SmcOutbox.Data;
using SmcOutbox.Infrastructure;
using SmcOutbox.Worker;
using SmcOutbox.Worker.Cron;
using SmcOutbox.Worker.Jobs;
using SmcOutbox.Worker.ServiceBus;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddApplication();
builder.Services.AddData();
builder.Services.AddInfrastructure();

builder.Services.AddHostedService<DomainEventsQueueListener>();

builder.Services.AddCronJob<AzureOutboxMessagesJob>(options =>
{
    options.Seconds = 10;
});

builder.Services.AddCronJob<HolaTierraExample>(options => { options.Seconds = 7; });
builder.Services.AddCronJob<HolaJupiterExample>(options => { options.Seconds = 10; });

var host = builder.Build();
host.Run();