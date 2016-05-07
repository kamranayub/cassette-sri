using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cassette.Scripts;

namespace Cassette.SubresourceIntegrity
{
    class ScriptIntegrityBundleHtmlRenderer : IBundleHtmlRenderer<ScriptBundle>
    {
        public ScriptIntegrityBundleHtmlRenderer(IUrlGenerator urlGenerator)
        {
            if (urlGenerator == null) throw new ArgumentNullException(nameof(urlGenerator));

            _urlGenerator = urlGenerator;
        }

        readonly IUrlGenerator _urlGenerator;

        public string Render(ScriptBundle bundle)
        {
            var content = $"<script src=\"{_urlGenerator.CreateBundleUrl(bundle)}\" " +
                          $"type=\"text/javascript\" " +
                          $"integrity=\"sha256-{Convert.ToBase64String(bundle.Hash)}\"" +
                          $"{bundle.HtmlAttributes.ToAttributeString()}></script>";

            var conditionalRenderer = new ConditionalRenderer();
            return conditionalRenderer.Render(
                bundle.Condition,
                html => html.Append(content)
            );
        }
    }
}
