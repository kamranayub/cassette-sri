using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
            string integrity;

            if (!IntegrityAttributeCache.TryGetValue(bundle, out integrity))
            {
                integrity = IntegrityAttributeCache.Add(bundle);
            }

            var content = $"<script src=\"{_urlGenerator.CreateBundleUrl(bundle)}\" " +
                          $"type=\"text/javascript\" " +
                          $"{integrity}{bundle.HtmlAttributes.ToAttributeString()}></script>";

            var conditionalRenderer = new ConditionalRenderer();
            return conditionalRenderer.Render(
                bundle.Condition,
                html => html.Append(content)
            );
        }
    }
}
