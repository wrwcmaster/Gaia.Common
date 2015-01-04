using System;

namespace CommonLib.Execute.Control
{
	public interface IExecutionControlSplitter
	{
		IExecutionControl getSplitControl(float portion);
	}
}

