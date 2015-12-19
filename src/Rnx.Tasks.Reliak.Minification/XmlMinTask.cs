using Rnx.Abstractions.Tasks;
using WebMarkupMin.Core;

namespace Rnx.Tasks.Reliak.Minification
{
    public class XmlMinTaskDescriptor : TaskDescriptorBase<XmlMinTask>
    {
        public XmlMinificationSettings Settings { get; }

        public XmlMinTaskDescriptor(XmlMinificationSettings settings = null)
        {
            Settings = settings;
        }
    }

    public class XmlMinTask : MinificationTask
    {
        private readonly XmlMinTaskDescriptor _taskDescriptor;

        public XmlMinTask(XmlMinTaskDescriptor taskDescriptor)
        {
            _taskDescriptor = taskDescriptor;
        }

        protected override string Minify(string input)
        {
            var xmlMin = new XmlMinifier(_taskDescriptor.Settings);
            return xmlMin.Minify(input).MinifiedContent;
        }
    }
}