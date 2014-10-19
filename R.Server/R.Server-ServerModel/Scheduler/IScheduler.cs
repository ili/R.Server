/*
 * Created by: Eugene Agafonov
 * Created: 29 ����� 2007 �.
 */

namespace R.Server.ServerModel
{
	/// <summary>
	/// Scheduler public contract
	/// </summary>
	public interface IScheduler
	{
		//
		//TODO: think about scheduler contract
		//

		/// <summary>
		/// Start scheduler service
		/// </summary>
		void Start();

		/// <summary>
		/// Stop scheduler service
		/// </summary>
		void Stop();
	}
}