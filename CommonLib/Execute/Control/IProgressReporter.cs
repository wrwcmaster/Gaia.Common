using System;

namespace CommonLib.Execute.Control
{
	public interface IProgressReporter
	{
		void PublishProgress(float percentage, String msg);
		event EventHandler<GenericEventArgs<ExecutionProgress>> OnProgressUpdate;
	}
}

