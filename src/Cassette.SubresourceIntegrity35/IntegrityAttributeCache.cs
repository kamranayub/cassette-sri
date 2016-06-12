using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Cassette.TinyIoC;

namespace Cassette.SubresourceIntegrity
{
    internal static class IntegrityAttributeCache
    {
        private static readonly SafeDictionary<string, string> CacheDict = new SafeDictionary<string, string>();

        public static bool TryGetValue(Bundle bundle, out string integrity)
        {
            return CacheDict.TryGetValue(bundle.Path, out integrity);
        }

        public static string Add(Bundle bundle)
        {
            string integrity;
            using (var stream = bundle.OpenStream())
            {
                using (var sha256 = SHA256.Create())
                {
                    integrity = $"integrity=\"sha256-{Convert.ToBase64String(sha256.ComputeHash(stream))}\" crossorigin=\"anonymous\"";
                    CacheDict[bundle.Path] = integrity;
                }
            }
            return integrity;            
        }
    }
}
