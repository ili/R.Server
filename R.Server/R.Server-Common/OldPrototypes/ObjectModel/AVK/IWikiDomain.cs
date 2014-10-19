using R.Server.Common.ObjectModel;

namespace R.Server.Common.ObjectModel
{
	public interface IWikiDomain : ISiteElement
	{
		string DisplayName { get; set; }

		string Description { get; set; }
	}
}