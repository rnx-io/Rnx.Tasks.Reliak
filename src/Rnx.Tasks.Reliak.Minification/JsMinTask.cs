using Rnx.Abstractions.Tasks;

namespace Rnx.Tasks.Reliak.Minification
{
    public class JsMinTaskDescriptor : TaskDescriptorBase<JsMinTask>
    { }

    public class JsMinTask : MinificationTask
    {
        protected override string Minify(string input)
        {
            var jsMinifier = new WebMarkupMin.Core.CrockfordJsMinifier();
            return jsMinifier.Minify(input, false).MinifiedContent;
        }
    }
}