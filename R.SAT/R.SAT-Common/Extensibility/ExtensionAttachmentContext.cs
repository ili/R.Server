using System;

namespace Rsdn.SmartApp
{
	internal class ExtensionAttachmentContext<T, A> : IExtensionAttachmentContext<T, A>
	{
		private readonly T _extensionType;
		private readonly IExtensionManager<T, A> _manager;

		public ExtensionAttachmentContext(IExtensionManager<T, A> manager, T extensionType)
		{
			_manager = manager;
			_extensionType = extensionType;
		}

		#region IExtensionAttachmentContext Members
		/// <summary>
		/// Менеджер расширений.
		/// </summary>
		public IExtensionManager<T, A> ExtensionManager
		{
			get { return _manager; }
		}

		/// <summary>
		/// Тип расширения.
		/// </summary>
		/// <remarks>null, если расширение связанно со сборкой.</remarks>
		public T ExtensionType
		{
			get { return _extensionType; }
		}

		/// <summary>
		/// Возвращает сервис, реализующий интерфейс T
		/// </summary>
		public object GetService(Type serviceType)
		{
			if (serviceType == typeof(IExtensionAttachmentContext<T, A>))
				return this;
			return ((IServiceProvider)_manager).GetService(serviceType);
		}
		#endregion
	}
}