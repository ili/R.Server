using System;

namespace Rsdn.SmartApp.Extensibility.Registration
{
	internal class NamedElementStrategy :
		RegKeyedElementsStrategy<string, TestNamedElementInfo, TestNamedElementAttribute>
	{
		public NamedElementStrategy(IServicePublisher publisher)
			: base(publisher)
		{
		}

		/// <summary>
		/// Создать элемент.
		/// </summary>
		public override TestNamedElementInfo CreateElement(
			IExtensionAttachmentContext<Type, Attribute> context,
			TestNamedElementAttribute attr)
		{
			return new TestNamedElementInfo(attr.Name, context.ExtensionType);
		}
	}
}