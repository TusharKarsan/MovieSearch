using Autofac;

namespace MovieDB
{
    public class MovieDbModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MovieData>().As<IMovieData>();
        }
    }
}
