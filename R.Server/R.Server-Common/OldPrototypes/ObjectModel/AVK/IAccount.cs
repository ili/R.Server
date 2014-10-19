namespace R.Server.Common.ObjectModel
{
	public interface IAccount : IRObject
	{
		IServerLogin ServerLogin { get; set; }

		int Nick { get; set; }
	}
}