using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaia.Common.Execute.Control
{
    public interface IExecutionProgress : ICloneable
    {
        float Percentage { get; set; }
        IExecutionProgress InnerProgress { get; set; }
    }
}
