using System.Reflection;

using Rsdn.SmartApp;

namespace R.Server.ServerModel
{
	/// <summary>
	/// RServer public contract.
	/// </summary>
	public interface IRServer
	{
		string Name { get; }

		string Description { get; }

		string WorkingDirectory { get; }

		Assembly[] LoadedExtensions { get; }

		event EventHandler<IRServer> Started;

		event EventHandler<IRServer> Stopped;

		event EventHandler<IRServer> Disposing;

		/// <summary>
		/// Run server.
		/// </summary>
		void Run(ServerStartedDelegate serverStartedDelegate);

		/// <summary>
		/// Stop server.
		/// </summary>
		void Stop();
	}

	public delegate void ServerStartedDelegate();
}