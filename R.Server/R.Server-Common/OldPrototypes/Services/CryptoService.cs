namespace R.Server.Common.Services
{
	public class CryptoService : ServiceBase<CryptoProvider>
	{
		static CryptoService()
		{
			LoadProviders("r.server/cryptoService");
		}

		public static string ComputeHash(string data, string salt)
		{
			return Provider.ComputeHash(data, salt);
		}

		public static string GenerateSalt()
		{
			return Provider.GenerateSalt();
		}

		public static string Encrypt(string data, string salt)
		{
			return Provider.Encrypt(data, salt);
		}

		public static string Decrypt(string data, string salt)
		{
			return Provider.Decrypt(data, salt);
		}
	}
}