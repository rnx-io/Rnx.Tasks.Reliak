using Rnx.Abstractions.Buffers;
using Rnx.Abstractions.Execution;
using Rnx.Abstractions.Tasks;
using Rnx.Tasks.Core.FileSystem;
using System.Collections.Generic;

namespace Rnx.Tasks.Reliak.Less
{
    public class LessTaskDescriptor : TaskDescriptorBase<LessTask>
    {
        public string NewExtension { get; private set; } = ".css";

        public LessTaskDescriptor WithExtension(string newExtension)
        {
            NewExtension = newExtension;
            return this;
        }
    }

    public class LessTask : RnxTask
    {
        private readonly LessTaskDescriptor _taskDescriptor;

        public LessTask(LessTaskDescriptor taskDescriptor)
        {
            _taskDescriptor = taskDescriptor;
        }

        public override void Execute(IBuffer input, IBuffer output, IExecutionContext executionContext)
        {
            foreach (var e in input.Elements)
            {
                e.Text = dotless.Core.Less.Parse(e.Text);

                if (e.Data.Exists<WriteFileData>())
                {
                    e.Data.Get<WriteFileData>().Extension = _taskDescriptor.NewExtension;
                }

                output.Add(e);
            }
        }
    }
}