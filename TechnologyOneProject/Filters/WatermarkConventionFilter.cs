using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace TechnologyOneProject.Filters
{
    public class WatermarkConventionFilter : IModelMetadataFilter
    {
        public void TransformMetadata(ModelMetadata metadata,
            IEnumerable<Attribute> attributes)
        {
            if (!string.IsNullOrEmpty(metadata.DisplayName) &&
                string.IsNullOrEmpty(metadata.Watermark))
            {
                metadata.Watermark = metadata.DisplayName + "...";
            }
        }
    }
}