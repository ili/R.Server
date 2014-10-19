using System;

namespace Rsdn.SmartApp.Extensibility
{
	internal class SimpleExtensionStrategy : AttachmentStrategyBase<SimpleExtensionAttribute>
	{
		private static Type _lastExtensionType;

		public static Type LastExtensionType
		{
			get { return _lastExtensionType; }
		}

		/// <summary>
		/// ���������� ����������.
		/// </summary>
		protected override void Attach(
			IExtensionAttachmentContext<Type, Attribute> context,
			SimpleExtensionAttribute attribute)
		{
			_lastExtensionType = context.ExtensionType;
		}
	}
}