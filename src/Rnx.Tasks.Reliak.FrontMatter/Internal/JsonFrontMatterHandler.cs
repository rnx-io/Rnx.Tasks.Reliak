using Newtonsoft.Json.Linq;
using Rnx.Abstractions.Util;
using System.Linq;

namespace Rnx.Tasks.Reliak.FrontMatter.Internal
{
    internal class JsonFrontMatterHandler : IFrontMatterHandler
    {
        public string StartLineText => "{";
        public string EndLineText => "}";
        public bool ExcludeStartAndEndLineFromFrontMatter => false;

        public void PopulateMetaData(string frontMatter, IDataStore dataStore)
        {
            var j = JObject.Parse(frontMatter);
            TraverseProperties(j, dataStore);
        }

        private static void TraverseProperties(JObject j, IDataStore dataStore)
        {
            foreach (var p in j.Properties())
            {
                if (p.Value is JObject)
                {
                    TraverseProperties((JObject)p.Value, dataStore);
                }
                else if (p.Value is JValue)
                {
                    var val = (JValue)p.Value;
                    dataStore[val.Path] = val.Value;
                }
                else if (p.Value is JArray)
                {
                    var arr = (JArray)p.Value;
                    var arrayValues = arr.OfType<JValue>().Select(f => f.Value).ToArray();

                    if (arrayValues.Any())
                    {
                        dataStore[arr.Path] = arrayValues;
                    }
                }
            }
        }
    }
}