using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Cassette.Stylesheets;

namespace Cassette.SubresourceIntegrity
{
    class DebugIntegrityStylesheetBundleHtmlRenderer : IBundleHtmlRenderer<StylesheetBundle>
    {
        public DebugIntegrityStylesheetBundleHtmlRenderer(IUrlGenerator urlGenerator)
        {
            if (urlGenerator == null) throw new ArgumentNullException(nameof(urlGenerator));

            _urlGenerator = urlGenerator;
        }

        readonly IUrlGenerator _urlGenerator;

        public string Render(StylesheetBundle bundle)
        {
            var createLink = GetCreateStyleFunc(bundle);
            var content = string.Join(
                Environment.NewLine,
                bundle.Assets.Select(createLink).ToArray()
            );

            var conditionalRenderer = new ConditionalRenderer();
            return conditionalRenderer.Render(
                bundle.Condition,
                html => html.Append(content)
            );
        }

        Func<IAsset, string> GetCreateStyleFunc(StylesheetBundle bundle)
        {
            return asset =>
            {
                string integrity;

                using (var stream = asset.OpenStream())
                {
                    using (var sha256 = SHA256.Create())
                    {
                        integrity = $"integrity=\"sha256-{Convert.ToBase64String(sha256.ComputeHash(stream))}\"";
                    }
                }

                return $"<link href=\"{_urlGenerator.CreateAssetUrl(asset)}\" " +
                       $"{integrity}{bundle.HtmlAttributes.ToAttributeString()}/>";
            };
        }
    }
}
