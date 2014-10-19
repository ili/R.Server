using R.Server.Common.ObjectModel;

namespace R.Server.Common.ObjectModel
{
	public interface ICommunityProject : ISiteElement
	{
		string Name { get; set; }

		string DisplayName { get; set; }

		string Description { get; set; }

		IArticle Article { get; set; }
	}
}