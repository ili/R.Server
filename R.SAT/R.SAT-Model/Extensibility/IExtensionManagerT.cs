namespace Rsdn.SmartApp
{
	/// <summary>
	/// Интерфейс менеджера расширений.
	/// </summary>
	/// <typeparam name="T">Тип описателя типа расширения</typeparam>
	/// <typeparam name="A">Тип метки расширения</typeparam>
	public interface IExtensionManager<T, A>
	{
		/// <summary>
		/// Сканирует сборку.
		/// </summary>
		void Scan(T[] types, IExtensionAttachmentStrategy<T, A> strategy);
	}
}
