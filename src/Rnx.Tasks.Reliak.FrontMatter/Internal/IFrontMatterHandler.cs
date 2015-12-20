using Rnx.Abstractions.Util;

namespace Rnx.Tasks.Reliak.FrontMatter.Internal
{
    public interface IFrontMatterHandler
    {
        string StartLineText { get; }
        string EndLineText { get; }
        bool ExcludeStartAndEndLineFromFrontMatter { get; }
        void PopulateMetaData(string frontMatter, IDataStore dataStore);
    }
}