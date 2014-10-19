using System;

namespace R.Server.Common
{
	internal struct UserNamePasswordPair : IEquatable<UserNamePasswordPair>
	{
		private readonly string _userName;
		private readonly string _password;

		public UserNamePasswordPair(string userName, string password)
		{
			_userName = userName;
			_password = password;
		}

		public string UserName
		{
			get { return _userName; }
		}

		public string Password
		{
			get { return _password; }
		}

		public bool Equals(UserNamePasswordPair userNamePasswordPair)
		{
			return Equals(_userName, userNamePasswordPair._userName) &&
				Equals(_password, userNamePasswordPair._password);
		}

		public override bool Equals(object obj)
		{
			if (!(obj is UserNamePasswordPair)) return false;
			return Equals((UserNamePasswordPair) obj);
		}

		public override int GetHashCode()
		{
			return _userName.GetHashCode() + 29*_password.GetHashCode();
		}
	}
}