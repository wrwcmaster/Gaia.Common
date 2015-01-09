using Gaia.Common.Event;
using System;

namespace Gaia.Common.Execute.Control
{
	public interface IProgressReporter
	{
		void PublishProgress(float percentage, String msg);
		event EventHandler<GenericEventArgs<ExecutionProgress>> OnProgressUpdate;
	}
}

