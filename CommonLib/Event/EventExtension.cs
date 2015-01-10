using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaia.Common.Event
{
    /// <summary>
    /// Extension for EventHandler delegate
    /// </summary>
    public static class EventExtension
    {
        /// <summary>
        /// A safer way to invoke event
        /// </summary>
        /// <typeparam name="T">EventArgs type</typeparam>
        /// <param name="eventMember">The event to invoke</param>
        /// <param name="sender">Event sender</param>
        /// <param name="eventArgs">Event args</param>
        public static void SafeInvoke<T>(this EventHandler<T> eventMember, object sender, T eventArgs) where T : EventArgs
        {
            EventHandler<T> eventVar = eventMember;
            if (eventVar != null) eventVar(sender, eventArgs);
        }

        /// <summary>
        /// A safer way to invoke event
        /// </summary>
        /// <param name="eventMember">The event to invoke</param>
        /// <param name="sender">Event sender</param>
        /// <param name="eventArgs">Event args</param>
        public static void SafeInvoke(this EventHandler eventMember, object sender, EventArgs eventArgs)
        {
            EventHandler eventVar = eventMember;
            if (eventVar != null) eventVar(sender, eventArgs);
        }
    }
}
