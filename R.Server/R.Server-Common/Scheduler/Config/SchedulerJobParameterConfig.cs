/*
 * Created by: Eugene Agafonov
 * Created: 8 апреля 2007 г.
 */

using System.Xml.Serialization;

using Rsdn.SmartApp.Configuration;

namespace R.Server.Common
{
	/// <summary>
	/// Scheduled task parameter configuration implementation for serialization.
	/// </summary>
	[XmlSerializerSection(typeof (SchedulerJobParameterConfig))]
	public class SchedulerJobParameterConfig : ISchedulerJobParameterConfig
	{
		#region IScheduledTaskParameterConfig Members
		[XmlAttribute("name")]
		public string Name { get; set; }

		[XmlText]
		public string Value { get; set; }
		#endregion
	}
}