namespace R.Server.Common
{
	public interface IServerEndpointConfig
	{
		string Name { get; }
		string Description { get; }
		string Transport { get; }
		string Path { get; }
		int Port { get; }
	}
}