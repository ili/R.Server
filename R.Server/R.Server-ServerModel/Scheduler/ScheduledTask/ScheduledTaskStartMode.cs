/*
 * Created by: Eugene Agafonov
 * Created: 29 марта 2007 г.
 */

namespace R.Server.ServerModel
{
    /// <summary>
    /// Scheduled task start mode
    /// </summary>
    public enum ScheduledTaskStartMode
    {
        Once,
        Hourly,
        Daily,
        Weekly,
        Monthly,
        Yearly
    }
}