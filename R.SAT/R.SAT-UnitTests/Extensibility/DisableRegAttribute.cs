using System;

namespace Rsdn.SmartApp.Extensibility
{
	[AttributeUsage(AttributeTargets.Class)]
	public class DisableRegAttribute : Attribute, IScanFilterAttribute<Type, Attribute>
	{
		private readonly string _disableName;

		public DisableRegAttribute(string disableName)
		{
			_disableName = disableName;
		}

		#region IScanFilterAttribute Members
		/// <summary>
		/// Возвращает true, если необходимо выполнить подключение расширения.
		/// </summary>
		public bool AllowAttach(
			IExtensionAttachmentContext<Type, Attribute> context,
			Attribute attribute)
		{
			var nae = attribute as NamedElementAttribute;
			return nae == null || nae.Name != _disableName;
		}
		#endregion
	}
}