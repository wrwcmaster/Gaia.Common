using Gaia.Common.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaia.Common.Execute.Control
{
    public class ExecutionControl : IExecutionControl
    {
        private float _totalPortionLeft = ExecutionProgress.MAX_PROGRESS;
        private IDictionary<IExecutionControl, float> _progressMap = new Dictionary<IExecutionControl, float>();
        
        private bool _isCancelled = false;
        public bool isCancelled()
        {
            return _isCancelled;
        }

        public event EventHandler<GenericEventArgs<IExecutionProgress>> OnProgressUpdate;
        public event EventHandler OnCancel;

        public ExecutionControl(ICancellationIndicator cancellationIndicator)
        {
            cancellationIndicator.OnCancel += (sender, e) =>
            {
                _isCancelled = true;
                OnCancel.SafeInvoke(sender, e);
            };

            _progressMap[this] = 0;
        }

        public void PublishProgress(IExecutionProgress progress)
        {
            float subProgress = (progress.Percentage * _totalPortionLeft / 100);
            _progressMap[this] = subProgress;
            var progressObj = progress.Clone() as IExecutionProgress;
            progressObj.Percentage = CalcCurrentProgress();
            OnProgressUpdate.SafeInvoke(this, new GenericEventArgs<IExecutionProgress>(progressObj));
        }

        private float CalcCurrentProgress()
        {
            float totalCompleted = 0;
            foreach (float i in _progressMap.Values)
            {
                totalCompleted += i;
            }
            return totalCompleted;
        }

        public IExecutionControl GetSplitControl(float portion)
        {
            float adjustedPortion;
            if(_totalPortionLeft < portion) adjustedPortion = _totalPortionLeft;
            else adjustedPortion = portion;
        
            _totalPortionLeft -= adjustedPortion;
        
            IExecutionControl subControl = new ExecutionControl(this);
            _progressMap[subControl] = 0;

            subControl.OnProgressUpdate += (sender, e) =>
            {
                float progress = (e.Item.Percentage * adjustedPortion / 100);
                _progressMap[subControl] = progress;
                OnProgressUpdate.SafeInvoke(this, new GenericEventArgs<IExecutionProgress>(new ExecutionProgress(CalcCurrentProgress()) { InnerProgress = e.Item }));
            };

            return subControl;
        }
    }
}
