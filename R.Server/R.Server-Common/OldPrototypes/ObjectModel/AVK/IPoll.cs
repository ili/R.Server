namespace R.Server.Common.ObjectModel
{
	public interface IPoll : ISiteElement
	{
		string DisplayName { get; set; }

		string Description { get; set; }

		int MaxSelectionCount { get; set; }
	}
}