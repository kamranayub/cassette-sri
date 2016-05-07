using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cassette.BundleProcessing;
using Cassette.Scripts;
using Cassette.Stylesheets;

namespace Cassette.SubresourceIntegrity
{
    public class InsertIntoPipelineSubresourceIntegrity : IBundlePipelineModifier<ScriptBundle>, IBundlePipelineModifier<StylesheetBundle>
    {
        public IBundlePipeline<ScriptBundle> Modify(IBundlePipeline<ScriptBundle> pipeline)
        {
            int i = pipeline.IndexOf<AssignScriptRenderer>();

            if (i >= 0)
            {
                pipeline.RemoveAt(i);
                pipeline.Insert<AssignIntegrityRenderer>(i);
            }

            ReplaceAssignHash(pipeline);
            return pipeline;
        }

        public IBundlePipeline<StylesheetBundle> Modify(IBundlePipeline<StylesheetBundle> pipeline)
        {
            int i = pipeline.IndexOf<AssignStylesheetRenderer>();

            if (i >= 0)
            {
                pipeline.RemoveAt(i);
                pipeline.Insert<AssignIntegrityRenderer>(i);
            }

            ReplaceAssignHash(pipeline);
            return pipeline;
        }

        private void ReplaceAssignHash<T>(IBundlePipeline<T> pipeline) where T : Bundle
        {
            var i = pipeline.IndexOf<AssignHash>();
            if (i >= 0)
            {
                pipeline.RemoveAt(i);
                pipeline.Insert<AssignHash256>(i);
            }
        }
    }
}
