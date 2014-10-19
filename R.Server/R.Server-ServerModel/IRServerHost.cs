namespace R.Server.ServerModel
{
	/// <summary>
	/// R.Server host interface.
	/// </summary>
	public interface IRServerHost
	{
		/// <summary>
		/// Restart server.
		/// </summary>
		void Restart(string reason);
	}
}