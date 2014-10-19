using BLToolkit.DataAccess;
using BLToolkit.Validation;

namespace R.Server.Common.ObjectModel
{
	public class AccountRole
	{
		[Required, PrimaryKey(1)] public int AccountID;
		[Required, PrimaryKey(2)] public int RoleID;
	}
}