/*
 * Created by: Eugene Agafonov 
 * Created: 1 апреля 2007 г.
 */

using System.Xml.Serialization;

using R.Common;

namespace R.Server.Common
{
	/// <summary>
	/// Scheduler configuration implementation for serialization only
	/// </summary>
	[XmlRoot(ConfigName, Namespace = ConfigNamespace)]
	public class SchedulerConfig : ISchedulerConfig
	{
		public const string ConfigName = "scheduler";
		public const string ConfigNamespace = CommonConstants.BaseXmlNamespace + "SchedulerConfig.xsd";

		private SchedulerJobConfig[] _tasks = new SchedulerJobConfig[0];

		[XmlElement("job", typeof (SchedulerJobConfig))]
		public SchedulerJobConfig[] Jobs
		{
			get { return _tasks; }
			set { _tasks = value ?? new SchedulerJobConfig[0]; }
		}

		#region ISchedulerConfig Members
		ISchedulerJobConfig[] ISchedulerConfig.Jobs
		{
			get { return _tasks; }
		}
		#endregion
	}
}