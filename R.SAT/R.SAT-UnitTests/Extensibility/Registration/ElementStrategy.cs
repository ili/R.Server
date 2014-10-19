using System;

namespace Rsdn.SmartApp.Extensibility.Registration
{
	internal class ElementStrategy : RegElementsStrategy<ElementInfo, ElementAttribute>
	{
		public ElementStrategy(IServicePublisher publisher)
			: base(publisher)
		{
		}

		/// <summary>
		/// Создать элемент.
		/// </summary>
		public override ElementInfo CreateElement(
			IExtensionAttachmentContext<Type, Attribute> context,
			ElementAttribute attr)
		{
			return new ElementInfo(context.ExtensionType);
		}
	}
}