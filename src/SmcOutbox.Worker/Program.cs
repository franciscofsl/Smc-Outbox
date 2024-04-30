using SmcOutbox.Application;
using SmcOutbox.Data;
using SmcOutbox.Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<DomainEventsQueueListener>();

builder.Services.AddApplication();
builder.Services.AddData();

var host = builder.Build();
host.Run();