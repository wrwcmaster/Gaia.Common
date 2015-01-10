using Gaia.Common.Event;
using System;

namespace Gaia.Common.Execute.Control
{
	public interface IProgressReporter
	{
		void PublishProgress(IExecutionProgress progress);
		event EventHandler<GenericEventArgs<IExecutionProgress>> OnProgressUpdate;
	}
}

