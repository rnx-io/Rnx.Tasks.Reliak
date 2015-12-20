using Rnx.Abstractions.Buffers;
using Rnx.Abstractions.Execution;
using Rnx.Abstractions.Tasks;
using Rnx.Abstractions.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Rnx.Tasks.Reliak.FrontMatter.Internal;

namespace Rnx.Tasks.Reliak.FrontMatter
{
    public class FrontMatterJsonTaskDescriptor : TaskDescriptorBase<FrontMatterJsonTask>
    { }

    public class FrontMatterJsonTask : FrontMatterTaskBase
    {
        protected override IFrontMatterHandler FrontMatterHandler => new JsonFrontMatterHandler();
    }
}