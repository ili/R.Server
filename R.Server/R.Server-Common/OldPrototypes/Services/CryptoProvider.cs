using System;
using System.Configuration.Provider;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace R.Server.Common.Services
{
	public class CryptoProvider : ProviderBase
	{
		#region Public Members

		public virtual string ComputeHash(string data, string salt)
		{
			HashAlgorithm alg = HashAlgorithm.Create(_hashAlgorithmType);

			byte[] ret = alg.ComputeHash(GetBuffer(data, salt));

			return Convert.ToBase64String(ret);
		}

		public virtual string GenerateSalt()
		{
			byte[] buf = new byte[16];

			new RNGCryptoServiceProvider().GetBytes(buf);

			return Convert.ToBase64String(buf);
		}

		public virtual string Encrypt(string data, string salt)
		{
			using (TripleDESCryptoServiceProvider sp = new TripleDESCryptoServiceProvider())
			using (MemoryStream ms = new MemoryStream())
			{
				using (CryptoStream cs = new CryptoStream(ms, sp.CreateEncryptor(_key, _iv), CryptoStreamMode.Write))
				using (StreamWriter sw = new StreamWriter(cs))
					sw.Write(data + salt);

				return Convert.ToBase64String(ms.ToArray());
			}
		}

		public virtual string Decrypt(string data, string salt)
		{
			byte[] buf = Convert.FromBase64CharArray(data.ToCharArray(), 0, data.Length);

			using (TripleDESCryptoServiceProvider sp = new TripleDESCryptoServiceProvider())
			using (MemoryStream ms = new MemoryStream(buf))
			using (CryptoStream cs = new CryptoStream(ms, sp.CreateDecryptor(_key, _iv), CryptoStreamMode.Read))
			using (StreamReader sr = new StreamReader(cs))
			{
				string value = sr.ReadToEnd();
				return value.Substring(0, value.Length - salt.Length);
			}
		}

		#endregion

		#region Protected Members

		private static string _hashAlgorithmType = "SHA1";

		private static byte[] _key = new byte[]
			{
				215,  87,  70, 127, 159, 164, 149, 104,
				18,  55,  91, 167, 143,  53, 216, 149,
				52, 221, 107,  11, 113,  28, 181, 17
			};

		private static byte[] _iv = new byte[]
			{
				191, 217, 39, 252, 12, 1, 118, 206
			};

		private static byte[] GetBuffer(string data, string salt)
		{
			byte[] bData = Encoding.Unicode.GetBytes(data);

			if (string.IsNullOrEmpty(salt))
				return bData;

			byte[] bSalt = Convert.FromBase64String(salt);
			byte[] bAll  = new byte[bData.Length + bSalt.Length];

			Buffer.BlockCopy(bSalt, 0, bAll, 0, bSalt.Length);
			Buffer.BlockCopy(bData, 0, bAll, bSalt.Length, bData.Length);

			return bAll;
		}

		#endregion
	}
}