using R.Server.Common.ObjectModel;

namespace R.Server.Common.ObjectModel
{
	/// <summary>
	/// Сайт
	/// </summary>
	public interface IRSite : IRObject
	{
		string Name { get; set; }

		string Description { get; set; }

		string RootUrl { get; set; }
	}
}