using Autofac;

namespace MovieIndex
{
    public class MovieIndexModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MovieIndex>().As<IMovieIndex>();
        }
    }
}
