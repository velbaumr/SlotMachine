﻿using Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Model;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Path.Combine(AppContext.BaseDirectory))
    .AddJsonFile("appsettings.json", true, true)
    .Build();

var services = new ServiceCollection()
    .Configure<SlotMachineOptions>(options => configuration.GetSection("SlotOptions").Bind(options))
    .AddSingleton<App>()
    .AddSingleton<ILoggingService, LoggingService>()
    .AddSingleton<ISlotMachineService, SlotMachineService>()
    .BuildServiceProvider();

var app = services.GetRequiredService<App>();
app.Run();