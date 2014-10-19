using System;

using R.Server.ServerModel;

namespace R.Server.ClientCommon
{
	public class RServerCredentials : IRServerCredentials
	{
		public const string AuthTypeSeparator = ":";
		public const string UserSeparator = "@";

		public RServerCredentials(string authenticationType, string login, string user, string password)
		{
			if (string.IsNullOrEmpty(authenticationType))
				throw new ArgumentNullException("authenticationType");

			AuthenticationType = authenticationType;
			Login = login;
			User = user;
			Password = password;
		}

		public RServerCredentials(string authenticationType, string login, string password)
			: this(authenticationType, login, "", password)
		{
		}

		public RServerCredentials(string fullName, string password)
		{
			var parts = fullName.Split(new[] {AuthTypeSeparator},
				StringSplitOptions.RemoveEmptyEntries);
			AuthenticationType = parts[0];
			if (parts.Length > 1)
			{
				var usi = parts[1].IndexOf(UserSeparator);
				if (usi >= 0)
				{
					if (usi == 0 || usi == parts[1].Length - UserSeparator.Length - 1)
						throw new ArgumentException("Invalid full name format.");
					Login = parts[1].Substring(0, usi);
					User = parts[1].Substring(usi + UserSeparator.Length);
				}
				else
				{
					Login = parts[1];
					User = "";
				}
			}
			else
			{
				Login = "";
				User = "";
			}
			Password = password;
		}

		public string AuthenticationType { get; private set; }
		public string Login { get; private set; }
		public string User { get; private set; }
		public string Password { get; private set; }

		public static string ToString(string authenticationType, string login, string user)
		{
			return authenticationType + AuthTypeSeparator + login
				+ (string.IsNullOrEmpty(user) ? "" : UserSeparator + user);
		}

		///<summary>
		///Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
		///</summary>
		///<returns>
		///A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
		///</returns>
		public override string ToString()
		{
			return ToString(AuthenticationType, Login, User);
		}
	}
}