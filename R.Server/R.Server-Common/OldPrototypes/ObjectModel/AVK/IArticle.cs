namespace R.Server.Common.ObjectModel
{
	public interface IArticle : ISiteElement
	{
		string Title { get; set; }

		string Author { get; set; }

		IArticleGroup ArticleGroup { get; set; }
	}
}