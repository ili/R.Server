using R.Server.Common.ObjectModel;

namespace R.Server.Common.ObjectModel
{
	/// <summary>
	/// Сообщение
	/// </summary>
	public interface IMessage : IContentObject
	{
		/// <summary>
		/// Ссылка на топик
		/// </summary>
		ITopic Topic { get; set; }

		/// <summary>
		/// Тема
		/// </summary>
		string Subject { get; set; }

		/// <summary>
		/// Текст
		/// </summary>
		string Text { get; set; }

		string Name { get; set; }
	}
}