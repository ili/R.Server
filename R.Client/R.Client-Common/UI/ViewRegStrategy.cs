using System;

using R.Client.Model;

using Rsdn.SmartApp;

namespace R.Client.Common
{
	public class ViewRegStrategy : RegElementsStrategy<ViewInfo, ViewAttribute>
	{
		public ViewRegStrategy(IServicePublisher publisher) : base(publisher)
		{
		}

		/// <summary>
		/// Создать элемент.
		/// </summary>
		public override ViewInfo CreateElement(
			IExtensionAttachmentContext<Type, Attribute> context,
			ViewAttribute attr)
		{
			if (!attr.ViewContract.IsAssignableFrom(context.ExtensionType))
				throw new ArgumentException("View '" + context.ExtensionType + "' must implement declared "
					+ "contract '" + attr.ViewContract + "'");
			return new ViewInfo(context.ExtensionType, attr.ViewContract, attr.Technology, attr.Layer);
		}
	}
}
