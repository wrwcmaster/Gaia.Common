using System;

namespace Gaia.CommonLib.Execute.Control
{
	public class ExecutionProgress
	{
		public const float MIN_PROGRESS = 0;
		public const float MAX_PROGRESS = 100;
		public float Percentage { get; set; }
		public String Message { get; set; }
		public ExecutionProgress ()
		{
		}
	}
}

