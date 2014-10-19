using System;

namespace R.Server.Common.ObjectModel
{
	public interface IServerLogin : IRObject
	{
		string UserName { get; set; }

		string PasswordHash { get; set; }

		DateTime LastLogin { get; set; }
	}
}