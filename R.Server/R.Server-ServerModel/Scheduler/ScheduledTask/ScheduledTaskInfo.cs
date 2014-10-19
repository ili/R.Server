/*
 * Created by: Eugene Agafonov
 * Created: 29 марта 2007 г.
 */

using System;

namespace R.Server.ServerModel
{
    /// <summary>
    /// Scheduled task info
    /// </summary>
    public class ScheduledTaskInfo : NamedElementInfo
    {
        public ScheduledTaskInfo(string name, Type type) : base(name, type)
        {
        }
    }
}