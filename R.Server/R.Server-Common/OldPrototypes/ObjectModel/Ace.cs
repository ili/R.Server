using BLToolkit.Common;
using BLToolkit.DataAccess;
using BLToolkit.Validation;

namespace R.Server.Common.ObjectModel
{
	public class Ace : EntityBase<Ace>
	{
		[Required, PrimaryKey(1)] public int        AceID;
		[Required, PrimaryKey(2)] public int        RoleID;
		public bool       Allow;
		public bool       Deny;
		[Required]                public AccessCode AccessCode;
	}
}