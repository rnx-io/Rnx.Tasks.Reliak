using Rnx.Abstractions.Util;
using System.Collections.Generic;
using System.IO;

namespace Rnx.Tasks.Reliak.FrontMatter.Internal
{
    internal class YamlFrontMatterHandler : IFrontMatterHandler
    {
        public string StartLineText => "---";
        public string EndLineText => "---";
        public bool ExcludeStartAndEndLineFromFrontMatter => true;

        public void PopulateMetaData(string frontMatter, IDataStore dataStore)
        {
            if(string.IsNullOrWhiteSpace(frontMatter))
            {
                return;
            }

            Dictionary<object, object> dict = null;

            using (var sr = new StringReader(frontMatter))
            {
                dict = new YamlDotNet.Serialization.Deserializer().Deserialize(sr) as Dictionary<object, object>;
            }

            if (dict != null)
            {
                TraverseElements(dict, dataStore, "");
            }
        }

        private static void TraverseElements(Dictionary<object, object> dict, IDataStore dataStore, string currentPath)
        {
            foreach (var kvp in dict)
            {
                var elementPath = currentPath.Length > 0 ? currentPath + "." + kvp.Key : kvp.Key.ToString();

                if (kvp.Value is Dictionary<object, object>)
                {
                    TraverseElements(kvp.Value as Dictionary<object, object>, dataStore, elementPath);
                }
                else
                {
                    dataStore[elementPath] = kvp.Value;
                }
            }
        }
    }
}