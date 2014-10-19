using BLToolkit.Common;
using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using BLToolkit.Validation;

namespace R.Server.Common.ObjectModel
{
	public class Acl : EntityBase<Acl>
	{
		[MapField("AclID"), PrimaryKey, NonUpdatable]
		[Required] public int ID;
	}
}