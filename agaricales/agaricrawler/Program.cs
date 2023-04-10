using Microsoft.Extensions.Hosting;
using System;
using IHost host = Host.CreateDefaultBuilder(args).Build();

Console.WriteLine("Hello, World!");

await host.RunAsync();
