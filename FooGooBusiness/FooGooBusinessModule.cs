using Autofac;

namespace FooGooBusiness
{
    public class FooGooBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FooManager>().As<IFooManager>();
            builder.RegisterType<FooEventManager>().As<IFooEventManager>();
            builder.RegisterType<FooEventJsonSerializationStrategy>().As<IFooEventSerializationStrategy>();

            base.Load(builder);
        }
    }
}