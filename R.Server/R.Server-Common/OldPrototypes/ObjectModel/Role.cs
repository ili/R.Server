using System.Collections.Generic;

using BLToolkit.Common;
using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using BLToolkit.Validation;

using R.Server.Common.ObjectModel;

namespace R.Server.Common.ObjectModel
{
	public class Role : EntityBase<Role>
	{
		[MapField("RoleID"), PrimaryKey, NonUpdatable]
		[Required]                public int      ID;
		[          MaxLength(50)] public string   Name;
		[Required]                public RoleType RoleType;

		private Dictionary<int,AccountRole> _accounts = new Dictionary<int, AccountRole>();
		public  Dictionary<int,AccountRole>  Accounts
		{
			get { return _accounts; }
		}

		[MapIgnore] public bool IsAnonym { get { return RoleType == RoleType.Anonym; } }
		[MapIgnore] public bool IsUser   { get { return RoleType == RoleType.User;   } }
		[MapIgnore] public bool IsCustom { get { return RoleType == RoleType.Custom; } }

		public bool IsAccountInRole(int id)
		{
			return Accounts.ContainsKey(id);
		}
	}
}