using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gaia.Common.Event;

namespace Gaia.Common.Execute.Control
{
    public class CancellationIndicator : ICancellationIndicator
    {
        private bool _isCancelled = false;
        public bool isCancelled()
        {
            return _isCancelled;
        }

        public event EventHandler OnCancel;

        public void Cancel()
        {
            _isCancelled = true;
            OnCancel.SafeInvoke(this, EventArgs.Empty);
        }
    }
}
