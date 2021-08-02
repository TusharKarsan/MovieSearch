using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MovieDB;
using MovieModels;
using System;

namespace MovieConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var host = Host.CreateDefaultBuilder(args)
                    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                    .ConfigureContainer<ContainerBuilder>(builder => {
                        builder.RegisterModule<MovieDbModule>();
                    })
                    .ConfigureServices((context, services) =>
                    {
                        var mapperConfig = new MapperConfiguration(mc =>
                        {
                            mc.AddProfile(new MovieDbMapperProfile());
                        });

                        IMapper mapper = mapperConfig.CreateMapper();

                        services
                            .AddSingleton(mapper)
                            .AddOptions()
                            .Configure<AppSettings>(context.Configuration.GetSection("appSettings"))
                            .AddSingleton(resolver => resolver.GetRequiredService<IOptions<AppSettings>>().Value)
                            .AddTransient<IStartUpConsole, StartUpConsole>();
                    })
                    .Build();

                IStartUpConsole consoleService = host.Services.GetRequiredService<IStartUpConsole>();
                consoleService.Run();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                Console.Error.WriteLine(ex.StackTrace);
            }
        }
    }
}
