using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace TechnologyOneProject.Infastructure
{
    public class StandardRegistry : Registry
    {
        public StandardRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
            });
        }
    }
}