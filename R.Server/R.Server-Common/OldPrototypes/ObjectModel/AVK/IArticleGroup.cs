namespace R.Server.Common.ObjectModel
{
	public interface IArticleGroup : ISiteElement
	{
		string DisplayName { get; set; }

		string Description { get; set; }

		IArticleGroup ParentGroup { get; set; }
	}
}