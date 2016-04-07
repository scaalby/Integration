using Nancy;
using Nancy.Authentication.Token;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.TinyIoc;

namespace MicroServicesSpike.Test
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
        }

        protected override IRootPathProvider RootPathProvider
        {
            get { return new SelfHostRootPathProvider(); }
        }

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            nancyConventions
                .StaticContentsConventions
                .Add(StaticContentConventionBuilder.AddDirectory("js", @"js"));

            nancyConventions
                .StaticContentsConventions
                .Add(StaticContentConventionBuilder.AddDirectory("css", @"css"));

            nancyConventions
                .StaticContentsConventions
                .Add(StaticContentConventionBuilder.AddDirectory("img", @"img"));

            nancyConventions
                .StaticContentsConventions
                .Add(StaticContentConventionBuilder.AddDirectory("fonts", @"fonts"));

            nancyConventions
                .StaticContentsConventions
                .Add(StaticContentConventionBuilder.AddDirectory("json",@"json"));

            base.ConfigureConventions(nancyConventions);
        }
        
        protected override void ConfigureApplicationContainer(Nancy.TinyIoc.TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
        }

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);

            TokenAuthentication.Enable(pipelines, new TokenAuthenticationConfiguration(container.Resolve<Tokenizer>(), null));
        }
    }
}
