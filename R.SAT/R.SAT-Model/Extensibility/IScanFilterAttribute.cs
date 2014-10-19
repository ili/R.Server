namespace Rsdn.SmartApp
{
	/// <summary>
	/// Интерфейс атрибута, запрещающего сортировку по указанному тпу параметров.
	/// </summary>
	public interface IScanFilterAttribute<T, A>
	{
		/// <summary>
		/// Возвращает true, если необходимо выполнить подключение расширения.
		/// </summary>
		bool AllowAttach(IExtensionAttachmentContext<T, A> context, A attribute);
	}
}