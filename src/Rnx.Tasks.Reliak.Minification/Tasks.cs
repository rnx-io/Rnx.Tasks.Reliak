using WebMarkupMin.Core;

namespace Rnx.Tasks.Reliak.Minification
{
    public static class Tasks
    {
        public static CssMinTaskDescriptor CssMin() => new CssMinTaskDescriptor();
        public static JsMinTaskDescriptor JsMin() => new JsMinTaskDescriptor();
        public static HtmlMinTaskDescriptor HtmlMin(HtmlMinificationSettings settings = null) => new HtmlMinTaskDescriptor(settings);
        public static XmlMinTaskDescriptor XmlMin(XmlMinificationSettings settings = null) => new XmlMinTaskDescriptor(settings);
    }
}