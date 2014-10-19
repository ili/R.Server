using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

using R.Server.ServerModel;

using Rsdn.SmartApp;

namespace R.Server.Common
{
	internal class RServerPrincipal : IRServerPrincipal
	{
		private readonly IRServerIdentity _identity;
		private readonly IDictionary<string, string> _roles;

		public RServerPrincipal(IRServerIdentity identity, IEnumerable<string> roles)
		{
			_identity = identity;
			_roles = roles.ToDictionary(value => value);
		}

		public IRServerIdentity identity
		{
			get { return _identity; }
		}

		#region IPrincipal Members
		///<summary>
		///Determines whether the current principal belongs to the specified role.
		///</summary>
		///
		///<returns>
		///true if the current principal is a member of the specified role; otherwise, false.
		///</returns>
		///
		///<param name="role">The name of the role for which to check membership. </param>
		public bool IsInRole(string role)
		{
			return _roles.ContainsKey(role);
		}

		///<summary>
		///Gets the identity of the current principal.
		///</summary>
		///
		///<returns>
		///The <see cref="T:System.Security.Principal.IIdentity"></see> object associated with the current principal.
		///</returns>
		///
		IIdentity IPrincipal.Identity
		{
			get { return _identity; }
		}
		#endregion
	}
}