using System;

using BLToolkit.Common;
using BLToolkit.Mapping;
using BLToolkit.DataAccess;

namespace R.Server.Common.ObjectModel
{
	public class AccountActivity : EntityBase<AccountActivity>
	{
		[MapField("AccountID"), PrimaryKey]
		public int      ID;
		public DateTime LastLoginDate;
		public DateTime LastActivityDate;
	}
}