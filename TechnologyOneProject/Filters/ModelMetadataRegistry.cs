using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace TechnologyOneProject.Filters
{
    public class ModelMetadataRegistry : Registry
    {
        public ModelMetadataRegistry()
        {
            For<ModelMetadataProvider>().Use<ExtensibleModelMetadataProvider>();

            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.AddAllTypesOf<IModelMetadataFilter>();
            });
        }
    }
}