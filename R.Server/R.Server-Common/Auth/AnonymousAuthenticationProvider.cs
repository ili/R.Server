using R.Server.ServerModel;

namespace R.Server.Common
{
	[AuthenticationProvider(ProviderName)]
	public class AnonymousAuthenticationProvider : IAuthenticationProvider
	{
		public const string ProviderName = "Anonymous";

		#region IAuthenticationProvider Members
		public bool Authenticate(string userName, string password)
		{
			// No check required for anonymous
			return true;
		}
		#endregion
	}
}