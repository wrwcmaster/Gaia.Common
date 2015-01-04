using System;

namespace Gaia.CommonLib
{
	public class GenericEventArgs<T> : EventArgs
	{
		public T Item { get; set; }
		public GenericEventArgs (T item)
		{
			Item = item;
		}
	}
}

