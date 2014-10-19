using System;

using R.Client.Model;

using Rsdn.SmartApp;

namespace R.Client.Common
{
	public class UIControllerRegStrategy
		: RegElementsStrategy<UIControllerInfo, UIControllerAttrbute>
	{
		public UIControllerRegStrategy(IServicePublisher publisher) : base(publisher)
		{
		}

		/// <summary>
		/// Создать элемент.
		/// </summary>
		public override UIControllerInfo CreateElement(
			IExtensionAttachmentContext<Type, Attribute> context,
			UIControllerAttrbute attr)
		{
			var ct = typeof (IUIController);
			if (!ct.IsAssignableFrom(context.ExtensionType))
				throw new ArgumentException("UI controller must implement '" + ct.FullName
					+ "' interface.");
			return new UIControllerInfo(context.ExtensionType, attr.ModelType, attr.Layer);
		}
	}
}
