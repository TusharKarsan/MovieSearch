using Autofac;
using MovieWebsite.CommandHandlers;

namespace MovieWebsite
{
    public class MovieWebsiteModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FindMoviesCommandHandler>().As<IFindMoviesCommandHandlers>();
            builder.RegisterType<GetYearsCommandHandler>().As<IGetYearsCommandHandler>();
            builder.RegisterType<GetLatestTopMoviesCommandHandler>().As<IGetLatestTopMoviesCommandHandler>();
            builder.RegisterType<GetGenresCommandHandler>().As<IGetGenresCommandHandler>();
            builder.RegisterType<ShowMovieInfoCommandHandler>().As<IShowMovieInfoCommandHandler>();
        }
    }
}
