namespace R.Server.Common.ObjectModel
{
	public interface IForumGroup : ISiteElement
	{
		IForumGroup ParentGroup { get; set; }

		string DisplayName { get; set; }

		string Description { get; set; }
	}
}