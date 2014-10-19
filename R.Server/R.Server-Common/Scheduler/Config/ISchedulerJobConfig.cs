/*
 * Created by: Eugene Agafonov
 * Created: 1 апреля 2007 г.
 */

namespace R.Server.Common
{
	/// <summary>
	/// Scheduled task configuration contract
	/// </summary>
	public interface ISchedulerJobConfig
	{
		string Task { get; }

		string Period { get; }

		ISchedulerJobParameterConfig[] Parameters { get; }
	}
}