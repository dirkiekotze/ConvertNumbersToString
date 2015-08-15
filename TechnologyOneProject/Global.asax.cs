using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TechnologyOneProject.Infastructure;
using StructureMap;


namespace TechnologyOneProject
{
    public class MvcApplication : HttpApplication
    {
        public IContainer Container
        {
            get
            {
                return (IContainer)HttpContext.Current.Items["_Container"];
            }
            set
            {
                HttpContext.Current.Items["_Container"] = value;
            }
        }


        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            DependencyResolver.SetResolver(new StructureMapDependencyResolver(() => Container ?? ObjectFactory.Container));

            ObjectFactory.Configure(cfg =>
            {
                cfg.AddRegistry(new StandardRegistry());
                //cfg.AddRegistry(new ControllerRegistry());
                //cfg.AddRegistry(new ActionFilterRegistry(() => Container ?? ObjectFactory.Container));
                //cfg.AddRegistry(new TaskRegistry());
                //cfg.AddRegistry(new MvcRegistry());
                //cfg.AddRegistry(new ModelMetadataRegistry());
                //cfg.For<IFilterProvider>().Use(
                //    new StructureMapFilterProvider(() => Container ?? ObjectFactory.Container));


            });
        }
    }
}