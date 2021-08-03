using Autofac;
using MovieWebsite.CommandHandlers;

namespace MovieWebsite
{
    public class MovieWebsiteModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GetYearsCommandHandler>().As<IGetYearsCommandHandler>();
            builder.RegisterType<GetGenresCommandHandler>().As<IGetGenresCommandHandler>();
        }
    }
}
