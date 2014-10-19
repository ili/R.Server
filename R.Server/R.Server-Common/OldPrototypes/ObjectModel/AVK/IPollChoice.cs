using R.Server.Common.ObjectModel;

namespace R.Server.Common.ObjectModel
{
	public interface IPollChoice : IContentObject
	{
		IPoll Poll { get; set; }

		string Text { get; set; }
	}
}