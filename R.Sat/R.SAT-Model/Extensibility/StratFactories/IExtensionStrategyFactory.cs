using System;

using JetBrains.Annotations;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// Интерфейс класса, предоставляющего специфические стратегии.
	/// </summary>
	public interface IExtensionStrategyFactory
	{
		/// <summary>
		/// Создает стратегии.
		/// </summary>
		[NotNull]
		IExtensionAttachmentStrategy[] CreateStrategies([NotNull] IServiceProvider provider);
	}
}