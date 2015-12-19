using System.Threading.Tasks;
using Rnx.Abstractions.Buffers;
using Rnx.Abstractions.Execution;
using Rnx.Abstractions.Tasks;

namespace Rnx.Tasks.Reliak.Minification
{
    public abstract class MinificationTask : RnxTask
    {
        protected abstract string Minify(string input);

        public override void Execute(IBuffer input, IBuffer output, IExecutionContext executionContext)
        {
            Parallel.ForEach(input.ElementsPartitioner, e =>
            {
                e.Text = Minify(e.Text);

                output.Add(e);
            });
        }
    }
}