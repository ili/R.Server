namespace R.Server.Common.ObjectModel
{
	public enum PasswordFormat : byte
	{
		Clear     = 0,
		Hashed    = 1,
		Encrypted = 2,
	}
}