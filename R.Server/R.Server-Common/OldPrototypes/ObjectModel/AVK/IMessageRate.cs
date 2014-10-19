using R.Server.Common.ObjectModel;

namespace R.Server.Common.ObjectModel
{
	public interface IMessageRate : IContentObject
	{
		IMessage Message { get; set; }

		int Rate { get; set; }
	}
}