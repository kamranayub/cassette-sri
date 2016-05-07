using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cassette.BundleProcessing;
using Cassette.Scripts;
using Cassette.Stylesheets;

namespace Cassette.SubresourceIntegrity
{
    public class AssignIntegrityRenderer : IBundleProcessor<ScriptBundle>, IBundleProcessor<StylesheetBundle>
    {
        readonly IUrlGenerator _urlGenerator;
        readonly CassetteSettings _settings;

        public AssignIntegrityRenderer(IUrlGenerator urlGenerator, CassetteSettings settings)
        {
            this._urlGenerator = urlGenerator;
            this._settings = settings;
        }

        public void Process(ScriptBundle bundle)
        {
            if (_settings.IsDebuggingEnabled)
            {
                bundle.Renderer = new DebugIntegrityScriptBundleHtmlRenderer(_urlGenerator);
            }
            else
            {
                bundle.Renderer = new ScriptIntegrityBundleHtmlRenderer(_urlGenerator);
            }
        }

        public void Process(StylesheetBundle bundle)
        {
            if (_settings.IsDebuggingEnabled)
            {
                bundle.Renderer = new DebugIntegrityStylesheetBundleHtmlRenderer(_urlGenerator);
            }
            else
            {
                bundle.Renderer = new StylesheetIntegrityBundleHtmlRenderer(_urlGenerator);
            }
        }
    }
}
