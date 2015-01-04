using System;

namespace CommonLib.Execute.Control
{
	public interface ICancellationIndicator
	{
		bool isCancelled();
		event EventHandler OnCancel;
	}
}

