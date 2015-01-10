using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaia.Plugin
{
    public class PluginInfoEventArgs : EventArgs
    {
        public PluginInfo PluginInfo
        {
            get;
            set;
        }
    }
}
