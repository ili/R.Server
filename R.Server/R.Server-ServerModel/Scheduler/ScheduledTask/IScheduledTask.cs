/*
 * Created by: Eugene Agafonov
 * Created: 29 марта 2007 г.
 */

using System;

namespace R.Server.ServerModel
{
    /// <summary>
    /// Scheduled task public contract
    /// </summary>
    public interface IScheduledTask
    {
        /// <summary>
        /// Start scheduled task
        /// </summary>
        /// <returns><see cref="IAsyncResult">operation result</see></returns>
        IAsyncResult StartTask();

        /// <summary>
        /// Task name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Date and time of first task start
        /// </summary>
        DateTime FirstStartTime { get; }

        /// <summary>
        /// <see cref="ScheduledTaskStartMode">Task start mode</see>
        /// </summary>
        ScheduledTaskStartMode StartMode { get; }

        /// <summary>
        /// <see cref="ScheduledTaskMode">Task current mode</see>
        /// </summary>
        ScheduledTaskMode Mode { get; set; }
    }
}