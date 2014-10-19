using System;
using System.Reflection;

namespace R.Client.Model
{
	/// <summary>
	/// Client host.
	/// </summary>
	public interface IClientHost : IServiceProvider
	{
		string Technology { get; }
		void Run();
		void Stop();
		Assembly[] LoadedExtensions { get; }
		string[] GetApplicationNames();
		IClientApplication GetApplication(string name);
	}
}
