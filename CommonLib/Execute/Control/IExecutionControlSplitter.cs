using System;

namespace Gaia.Common.Execute.Control
{
	public interface IExecutionControlSplitter
	{
		IExecutionControl getSplitControl(float portion);
	}
}

