using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cassette.SubresourceIntegrity
{
    public static class HtmlAttributeDictionaryExtensions
    {
        public static string ToAttributeString(this HtmlAttributeDictionary dict)
        {
            var builder = new StringBuilder(256);
            foreach (var attribute in dict)
            {
                AppendAttribute(builder, attribute);
            }
            return builder.ToString();
        }

        static void AppendAttribute(StringBuilder builder, KeyValuePair<string, string> attribute)
        {
            if (attribute.Value == null)
            {
                builder.AppendFormat(" {0}", attribute.Key);
            }
            else
            {
                builder.AppendFormat(" {0}=\"{1}\"", attribute.Key, attribute.Value);
            }
        }
    }
}
