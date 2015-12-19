using Rnx.Abstractions.Tasks;

namespace Rnx.Tasks.Reliak.Minification
{
    public class CssMinTaskDescriptor : TaskDescriptorBase<CssMinTask>
    { }

    public class CssMinTask : MinificationTask
    {
        protected override string Minify(string input)
        {
            var cssMinifier = new WebMarkupMin.Core.KristensenCssMinifier();
            return cssMinifier.Minify(input, false).MinifiedContent;
        }
    }
}
