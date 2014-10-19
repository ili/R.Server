using BLToolkit.Common;
using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using BLToolkit.Validation;

namespace R.Server.Common.ObjectModel
{
	public class AccountInfoType : EntityBase<AccountInfoType>
	{
		[MapField("AccountInfoTypeID"), PrimaryKey, NonUpdatable]
		[Required]                public int    ID;
		[Required, MaxLength(50)] public string Code;
		[Required, MaxLength(50)] public string Name;
		[Required, MaxLength(50)] public string Description;
	}
}