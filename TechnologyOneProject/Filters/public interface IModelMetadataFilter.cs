using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace TechnologyOneProject.Filters
{
    public interface IModelMetadataFilter
    {
        void TransformMetadata(ModelMetadata metadata,
            IEnumerable<Attribute> attributes);
    }
}