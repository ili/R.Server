namespace R.Server.Common
{
	public interface ILogTargetConfig
	{
		string Name { get; }
		ITargetParameterConfig[] Parameters { get; }
	}
}