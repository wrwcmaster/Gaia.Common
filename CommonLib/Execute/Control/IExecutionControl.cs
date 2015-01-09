using System;

namespace Gaia.Common.Execute.Control
{
	public interface IExecutionControl : IProgressReporter, ICancellationIndicator, IExecutionControlSplitter
	{
	}
}

