namespace R.Server.Common
{
	public interface IDBDescriptor
	{
		string Name { get; }

		string DriverName { get; }

		string ConnectionString { get; }
	}
}