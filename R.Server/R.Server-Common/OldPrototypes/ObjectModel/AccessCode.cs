namespace R.Server.Common.ObjectModel
{
	public enum AccessCode : byte
	{
		Read       = 0,
		Write      = 1,
		Create     = 2,
		Delete     = 3,
		Permission = 4
	}
}