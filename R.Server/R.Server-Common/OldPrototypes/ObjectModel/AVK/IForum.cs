namespace R.Server.Common.ObjectModel
{
	public interface IForum : ISiteElement
	{
		string Name { get; set; }

		string DisplayName { get; set; }

		string Description { get; set; }

		IForumGroup ForumGroup { get; set; }
	}
}