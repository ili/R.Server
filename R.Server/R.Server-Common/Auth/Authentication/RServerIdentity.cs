using R.Server.ServerModel;

namespace R.Server.Common
{
	internal class RServerIdentity : IRServerIdentity
	{
		private readonly string _name;
		private readonly string _authenticationType;
		private readonly string _user;

		public RServerIdentity(string name, string authenticationType, string user)
		{
			_name = name;
			_authenticationType = authenticationType;
			_user = user;
		}

		#region IRServerIdentity Members
		public string User
		{
			get { return _user; }
		}
		#endregion

		#region IIdentity Members
		///<summary>
		///Gets the name of the current user.
		///</summary>
		///
		///<returns>
		///The name of the user on whose behalf the code is running.
		///</returns>
		public string Name
		{
			get { return _name; }
		}

		///<summary>
		///Gets the type of authentication used.
		///</summary>
		///
		///<returns>
		///The type of authentication used to identify the user.
		///</returns>
		public string AuthenticationType
		{
			get { return _authenticationType; }
		}

		///<summary>
		///Gets a value that indicates whether the user has been authenticated.
		///</summary>
		///
		///<returns>
		///true if the user was authenticated; otherwise, false.
		///</returns>
		public bool IsAuthenticated
		{
			get { return true; }
		}
		#endregion
	}
}