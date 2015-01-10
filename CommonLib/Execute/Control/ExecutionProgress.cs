using System;

namespace Gaia.Common.Execute.Control
{
	public class ExecutionProgress : IExecutionProgress
	{
		public const float MIN_PROGRESS = 0;
		public const float MAX_PROGRESS = 100;
		public float Percentage { get; set; }
        public IExecutionProgress InnerProgress { get; set; }
		public ExecutionProgress (float percentage)
		{
            Percentage = percentage;
		}

        public virtual object Clone()
        {
            return new ExecutionProgress(Percentage) { InnerProgress = InnerProgress };
        }
    }
}

