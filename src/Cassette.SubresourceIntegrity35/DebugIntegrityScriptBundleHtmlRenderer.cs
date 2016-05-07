using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Cassette.Scripts;

namespace Cassette.SubresourceIntegrity
{
    class DebugIntegrityScriptBundleHtmlRenderer : IBundleHtmlRenderer<ScriptBundle>
    {
        public DebugIntegrityScriptBundleHtmlRenderer(IUrlGenerator urlGenerator)
        {
            if (urlGenerator == null) throw new ArgumentNullException(nameof(urlGenerator));

            _urlGenerator = urlGenerator;
        }

        readonly IUrlGenerator _urlGenerator;

        public string Render(ScriptBundle bundle)
        {
            var createLink = GetCreateScriptFunc(bundle);
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

        Func<IAsset, string> GetCreateScriptFunc(ScriptBundle bundle)
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

                return $"<script src=\"{_urlGenerator.CreateAssetUrl(asset)}\" " +
                       $"type=\"text/javascript\" " +
                       $"{integrity}{bundle.HtmlAttributes.ToAttributeString()}></script>";
            };
        }
    }
}
