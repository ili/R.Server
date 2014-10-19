using R.Server.Common.ObjectModel;

namespace R.Server.Common.ObjectModel
{
	public interface ISiteElement : IContentObject
	{
		IRSite RSite { get; set; }
	}
}