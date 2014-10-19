/*
 * Created by: Eugene Agafonov  
 * Created: 2 апреля 2007 г.
 */

using System.Xml.Serialization;

using Rsdn.SmartApp.Configuration;

namespace R.Server.Common
{
	/// <summary>
	/// Scheduled task configuration implementation for serialization.
	/// </summary>
	[XmlSerializerSection(typeof (SchedulerJobConfig))]
	public class SchedulerJobConfig : ISchedulerJobConfig
	{
		private SchedulerJobParameterConfig[] _params = new SchedulerJobParameterConfig[0];

		[XmlElement("param", typeof (SchedulerJobParameterConfig))]
		public SchedulerJobParameterConfig[] Params
		{
			get { return _params; }
			set { _params = value ?? new SchedulerJobParameterConfig[0]; }
		}

		#region IScheduledTaskConfig Members
		ISchedulerJobParameterConfig[] ISchedulerJobConfig.Parameters
		{
			get { return _params; }
		}

		[XmlAttribute("task")]
		public string Task { get; set; }

		[XmlElement("period")]
		public string Period { get; set; }
		#endregion
	}
}