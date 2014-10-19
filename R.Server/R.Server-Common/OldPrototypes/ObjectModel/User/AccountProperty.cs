using BLToolkit.DataAccess;
using BLToolkit.Validation;

namespace R.Server.Common.ObjectModel
{
	/// <summary>
	/// Represent dynamic account property like "RealName", "origin", ect..
	/// In DB this class split by AccountInfoType & AccountInfo table
	/// </summary>
	public class AccountProperty
	{
		[PrimaryKey(1)] 
		public int		AccountId;

		[PrimaryKey(2)] 
		public int		AccountInfoTypeId;

		[Required] 
		[MaxLength(50)] 
		public string	Code;

		[Required] 
		[MaxLength(50)] 
		public string	Name;

		[Required]
		[MaxLength(50)] 
		public string	Description;

		[Required]
		[MaxLength(4000)] 
		public string	Value;
	}
}