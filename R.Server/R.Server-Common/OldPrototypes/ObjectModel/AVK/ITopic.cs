using R.Server.Common.ObjectModel;

namespace R.Server.Common.ObjectModel
{
	public interface ITopic : IMessage
	{
		ISiteElement ParentElement { get; set; }
	}
}