using System;

namespace Gaia.CommonLib.Execute.Control
{
	public interface ICancellationIndicator
	{
		bool isCancelled();
		event EventHandler OnCancel;
	}
}

