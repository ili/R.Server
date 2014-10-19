/*
 * Created by: Eugene Agafonov
 * Created: 1 апреля 2007 г.
 */

using Rsdn.SmartApp.Configuration;

namespace R.Server.Common
{
	/// <summary>
	/// Scheduler configuration contract
	/// </summary>
	[XmlSerializerSection(typeof (SchedulerConfig),
		SchemaResource = "R.Server.Common.Scheduler.Config.SchedulerConfig.xsd")]
	public interface ISchedulerConfig
	{
		ISchedulerJobConfig[] Jobs { get; }
	}
}