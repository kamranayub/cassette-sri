using System;
using System.IO;
using System.Security.Cryptography;
using Cassette.BundleProcessing;
using Cassette.Utilities;

namespace Cassette.SubresourceIntegrity
{
    public class AssignHash256 : IBundleProcessor<Bundle>
    {
        public void Process(Bundle bundle)
        {
            using (var concatenatedStream = new MemoryStream())
            {
                bundle.Accept(new ConcatenatedStreamBuilder(concatenatedStream));

                concatenatedStream.Position = 0;
                bundle.Hash = ComputeSha256Hash(concatenatedStream);
            }
        }

        byte[] ComputeSha256Hash(Stream concatenatedStream)
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(concatenatedStream);
            }
        }

        class ConcatenatedStreamBuilder : IBundleVisitor
        {
            readonly Stream concatenatedStream;

            public ConcatenatedStreamBuilder(Stream concatenatedStream)
            {
                this.concatenatedStream = concatenatedStream;
            }

            public void Visit(Bundle bundle)
            {
            }

            public void Visit(IAsset asset)
            {
                using (var stream = asset.OpenStream())
                {
                    stream.CopyTo(concatenatedStream);
                }
            }
        }
    }
}
