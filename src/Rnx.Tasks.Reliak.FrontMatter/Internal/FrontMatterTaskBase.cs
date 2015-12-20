using Rnx.Abstractions.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rnx.Abstractions.Buffers;
using Rnx.Abstractions.Execution;

namespace Rnx.Tasks.Reliak.FrontMatter.Internal
{
    public abstract class FrontMatterTaskBase : RnxTask
    {
        protected abstract IFrontMatterHandler FrontMatterHandler { get; }

        public override void Execute(IBuffer input, IBuffer output, IExecutionContext executionContext)
        {
            var executer = new FrontMatterExecuter(FrontMatterHandler);

            foreach (var e in input.Elements)
            {
                executer.Execute(e);

                output.Add(e);
            }
        }
    }
}