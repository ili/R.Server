namespace Rsdn.SmartApp
{
	/// <summary>
	/// Стратегия подключения расширения.
	/// </summary>
	/// <typeparam name="T">Тип описателя типа расширения</typeparam>
	/// <typeparam name="A">Тип метки расширения</typeparam>
	public interface IExtensionAttachmentStrategy<T, A>
	{
		/// <summary>
		/// Подключает расширение.
		/// </summary>
		void Attach(IExtensionAttachmentContext<T, A> context, A attribute);
	}
}
