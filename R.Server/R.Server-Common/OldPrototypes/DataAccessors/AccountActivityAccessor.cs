using System;
using System.Collections.Generic;

using R.Server.Common.ObjectModel;

namespace R.Server.Common.DataAccessors
{
	public abstract class AccountActivityAccessor : SupportDataAccessor<AccountActivity>
	{
		public abstract void      UpdateActivityDate   (int @id, DateTime @currentUtcDate);
		public abstract int       GetActiveCountSince  (DateTime @activeSinceDate);
		public abstract int       GetInactiveCountSince(DateTime @inactiveSinceDate);
		public abstract List<int> GetInactiveList      (DateTime @inactiveSinceDate);
	}
}