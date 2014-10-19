using System;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// Контекст инициализации расширения.
	/// </summary>
	/// <typeparam name="T">Тип описателя типа расширения</typeparam>
	/// <typeparam name="A">Тип метки расширения</typeparam>
	public interface IExtensionAttachmentContext<T, A> : IServiceProvider
	{
		/// <summary>
		/// Менеджер расширений.
		/// </summary>
		IExtensionManager<T, A> ExtensionManager { get; }

		/// <summary>
		/// Тип расширения.
		/// </summary>
		/// <remarks>null, если расширение связанно со сборкой.</remarks>
		T ExtensionType { get; }
	}
}
