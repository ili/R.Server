/*
 * Created by: Eugene Agafonov
 * Created: 8 апреля 2007 г.
 */

namespace R.Server.Common
{
	public interface ISchedulerJobParameterConfig
	{
		string Name { get; }
		string Value { get; }
	}
}