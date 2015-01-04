using System;

namespace Gaia.CommonLib.Execute.Control
{
	public interface IExecutionControl : IProgressReporter, ICancellationIndicator, IExecutionControlSplitter
	{
	}
}

