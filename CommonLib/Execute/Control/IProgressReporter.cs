using System;

namespace Gaia.CommonLib.Execute.Control
{
	public interface IProgressReporter
	{
		void PublishProgress(float percentage, String msg);
		event EventHandler<GenericEventArgs<ExecutionProgress>> OnProgressUpdate;
	}
}

