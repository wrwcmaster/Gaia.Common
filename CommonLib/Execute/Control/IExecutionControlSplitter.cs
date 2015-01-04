using System;

namespace Gaia.CommonLib.Execute.Control
{
	public interface IExecutionControlSplitter
	{
		IExecutionControl getSplitControl(float portion);
	}
}

