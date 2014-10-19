using R.Server.Common.ObjectModel;

namespace R.Server.Common.ObjectModel
{
	public interface ILogin
	{
		int			LoginId		{ get; set; }
		
		int			AccountId	{ get; set; }
		
		LoginType	LoginType	{ get; }
	}
}