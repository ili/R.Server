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
		/// �������� ����������.
		/// </summary>
		public IExtensionManager<T, A> ExtensionManager
		{
			get { return _manager; }
		}

		/// <summary>
		/// ��� ����������.
		/// </summary>
		/// <remarks>null, ���� ���������� �������� �� �������.</remarks>
		public T ExtensionType
		{
			get { return _extensionType; }
		}

		/// <summary>
		/// ���������� ������, ����������� ��������� T
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