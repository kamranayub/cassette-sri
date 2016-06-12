using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Cassette.Stylesheets;

namespace Cassette.SubresourceIntegrity
{
    class StylesheetIntegrityBundleHtmlRenderer : IBundleHtmlRenderer<StylesheetBundle>
    {
        public StylesheetIntegrityBundleHtmlRenderer(IUrlGenerator urlGenerator)
        {
            if (urlGenerator == null) throw new ArgumentNullException(nameof(urlGenerator));

            _urlGenerator = urlGenerator;
        }

        readonly IUrlGenerator _urlGenerator;

        public string Render(StylesheetBundle bundle)
        {
            string integrity;

            if (!IntegrityAttributeCache.TryGetValue(bundle, out integrity))
            {
                integrity = IntegrityAttributeCache.Add(bundle);
            }
            
            var content = $"<link href=\"{_urlGenerator.CreateBundleUrl(bundle)}\" " +
                          $"{integrity}{bundle.HtmlAttributes.ToAttributeString()}/>";

            var conditionalRenderer = new ConditionalRenderer();
            return conditionalRenderer.Render(
                bundle.Condition,
                html => html.Append(content)
            );
        }
    }
}
