using System;

using R.Server.ServerModel;

using Rsdn.SmartApp;

namespace R.Server.Common
{
	public class CommTransportRegStrategy
		: RegKeyedElementsStrategy<string, CommTransportProviderInfo, CommTransportProviderAttribute>
	{
		public CommTransportRegStrategy(IServicePublisher publisher) : base(publisher)
		{
		}

		/// <summary>
		/// Создать элемент.
		/// </summary>
		public override CommTransportProviderInfo CreateElement(
			IExtensionAttachmentContext<Type, Attribute> context,
			CommTransportProviderAttribute attr)
		{
			var provContract = typeof (ICommTransportProvider);
			if (!provContract.IsAssignableFrom(context.ExtensionType))
				throw new ArgumentException("Communication transport provider must implement '"
					+ provContract.FullName + "' interface");
			return new CommTransportProviderInfo(attr.Name, context.ExtensionType);
		}
	}
}