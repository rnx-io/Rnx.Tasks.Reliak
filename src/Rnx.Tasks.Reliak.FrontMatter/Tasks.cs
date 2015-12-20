namespace Rnx.Tasks.Reliak.FrontMatter
{
    public static class Tasks
    {
        public static FrontMatterJsonTaskDescriptor JsonFrontMatter() => new FrontMatterJsonTaskDescriptor();
        public static FrontMatterYamlTaskDescriptor YamlFrontMatter() => new FrontMatterYamlTaskDescriptor();
    }
}