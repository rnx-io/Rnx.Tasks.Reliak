using Rnx.Abstractions.Buffers;
using Rnx.Abstractions.Tasks;
using Rnx.Tasks.Reliak.FrontMatter.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rnx.Tasks.Reliak.FrontMatter
{
    public class FrontMatterYamlTaskDescriptor : TaskDescriptorBase<FrontMatterYamlTask>
    { }

    public class FrontMatterYamlTask : FrontMatterTaskBase
    {
        protected override IFrontMatterHandler FrontMatterHandler => new YamlFrontMatterHandler();
    }
}