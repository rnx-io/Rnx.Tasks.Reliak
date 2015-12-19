using Rnx.Abstractions.Tasks;
using WebMarkupMin.Core;

namespace Rnx.Tasks.Reliak.Minification
{
    public class HtmlMinTaskDescriptor : TaskDescriptorBase<HtmlMinTask>
    {
        public HtmlMinificationSettings Settings { get; }

        public HtmlMinTaskDescriptor(HtmlMinificationSettings settings = null)
        {
            Settings = settings;
        }
    }

    public class HtmlMinTask : MinificationTask
    {
        private readonly HtmlMinTaskDescriptor _taskDescriptor;

        public HtmlMinTask(HtmlMinTaskDescriptor taskDescriptor)
        {
            _taskDescriptor = taskDescriptor;
        }

        protected override string Minify(string input)
        {
            var jsMinifier = new CrockfordJsMinifier();
            var cssMinifier = new KristensenCssMinifier();
            var htmlMinifier = new HtmlMinifier(settings: _taskDescriptor.Settings, cssMinifier: cssMinifier, jsMinifier: jsMinifier);

            return htmlMinifier.Minify(input).MinifiedContent;
        }
    }
}