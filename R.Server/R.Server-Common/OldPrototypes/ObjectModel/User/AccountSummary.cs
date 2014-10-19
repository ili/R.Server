using System;

using BLToolkit.Common;
using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using BLToolkit.Validation;

namespace R.Server.Common.ObjectModel
{
	public class AccountSummary : EntityBase<AccountSummary>
	{
		[MapField("AccountID"), Required, PrimaryKey, NonUpdatable] 
		public int AccountId;

		public int Raiting;
		public int PersonalRates;
		public int TotalRates;

		public DateTime LastLoginTime;
		public DateTime LastActivityTime;
		public DateTime LastUpdateTime;
	}
}