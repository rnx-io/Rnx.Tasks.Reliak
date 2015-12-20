using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rnx.Tasks.Reliak.FrontMatter.Internal
{
    internal class TextInfo
    {
        public string FrontMatter { get; }
        public string MainText { get; }

        public TextInfo(string frontMatter, string mainText)
        {
            FrontMatter = frontMatter;
            MainText = mainText;
        }
    }
}
