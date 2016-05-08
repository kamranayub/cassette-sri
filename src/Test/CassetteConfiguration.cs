using Cassette;
using Cassette.Scripts;
using Cassette.Stylesheets;

namespace Test
{
    /// <summary>
    /// Configures the Cassette asset bundles for the web application.
    /// </summary>
    public class CassetteBundleConfiguration : IConfiguration<BundleCollection>
    {
        public void Configure(BundleCollection bundles)
        {
            bundles.AddPerIndividualFile<StylesheetBundle>("Assets");
            bundles.AddPerIndividualFile<ScriptBundle>("Assets");            
        }
    }
}