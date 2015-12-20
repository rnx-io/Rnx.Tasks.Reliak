using Rnx.Abstractions.Buffers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Rnx.Tasks.Reliak.FrontMatter.Internal
{
    internal class FrontMatterExecuter
    {
        private readonly IFrontMatterHandler _frontMatterHandler;

        public FrontMatterExecuter(IFrontMatterHandler frontMatterHandler)
        {
            _frontMatterHandler = frontMatterHandler;
        }

        public void Execute(IBufferElement e)
        {
            var textInfo = GetTextInfo(e.Text);

            if (textInfo != null)
            {
                e.Text = textInfo.MainText;
                _frontMatterHandler.PopulateMetaData(textInfo.FrontMatter, e.Data);
            }
        }

        private TextInfo GetTextInfo(string text)
        {
            using (var sr = new StringReader(text.Trim()))
            {
                var line = sr.ReadLine(); // read first line

                if(line == null || !string.Equals(_frontMatterHandler.StartLineText, line))
                {
                    return null;
                }

                var foundEnd = false;
                var frontMatterLines = new List<string>();
                frontMatterLines.Add(line); // add the first line

                while (!foundEnd && (line = sr.ReadLine()) != null)
                {
                    foundEnd = string.Equals(_frontMatterHandler.EndLineText, line);
                    frontMatterLines.Add(line);
                }

                if (foundEnd)
                {
                    if(_frontMatterHandler.ExcludeStartAndEndLineFromFrontMatter)
                    {
                        // remove the start line
                        frontMatterLines.RemoveAt(0);

                        if(frontMatterLines.Any())
                        {
                            // remove the end line
                            frontMatterLines.RemoveAt(frontMatterLines.Count - 1);
                        }
                    }

                    var frontMatter = string.Join(Environment.NewLine, frontMatterLines.ToArray());
                    return new TextInfo(frontMatter, sr.ReadToEnd());
                }
            }

            return null;
        }
    }
}