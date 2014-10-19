using BLToolkit.Common;
using BLToolkit.DataAccess;
using BLToolkit.Validation;

namespace R.Server.Common.ObjectModel
{
	public class AccountInfo : EntityBase<AccountInfo>
	{
		[PrimaryKey(1)]             public int    AccountID;
		[PrimaryKey(2)]             public int    AccountInfoTypeID;
		[Required, MaxLength(4000)] public string Value;
	}
}