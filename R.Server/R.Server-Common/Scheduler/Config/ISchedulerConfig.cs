/*
 * Created by: Eugene Agafonov
 * Created: 1 ������ 2007 �.
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