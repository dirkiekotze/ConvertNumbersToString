using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechnologyOneProject.Filters
{
    public interface IModelMetadataFilter
    {
        void TransformMetadata(System.Web.Mvc.ModelMetadata metadata,
            IEnumerable<Attribute> attributes);
    }
}