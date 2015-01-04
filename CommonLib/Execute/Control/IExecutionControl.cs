using System;

namespace CommonLib.Execute.Control
{
	public interface IExecutionControl : IProgressReporter, ICancellationIndicator, IExecutionControlSplitter
	{
	}
}

