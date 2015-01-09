using System;

namespace Gaia.Common.Execute.Control
{
	public interface ICancellationIndicator
	{
		bool isCancelled();
		event EventHandler OnCancel;
	}
}

