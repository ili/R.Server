using System;

namespace Rsdn.SmartApp.Demos
{
	internal class RegFruitStrategy : RegElementsStrategy<FruitInfo, FruitAttribute>
	{
		public RegFruitStrategy(IServicePublisher publisher) : base(publisher)
		{}

		/// <summary>
		/// Создать элемент.
		/// </summary>
		public override FruitInfo CreateElement(
			IExtensionAttachmentContext<Type, Attribute> context,
			FruitAttribute attr)
		{
			return new FruitInfo(attr.Name, context.ExtensionType);
		}
	}
}
