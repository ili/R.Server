using System;

using R.Client.Model;

using Rsdn.SmartApp;

namespace R.Client.Common
{
	public class ClientAppStrategy
		: RegKeyedElementsStrategy<string, ClientApplicationInfo, ClientApplicationAttribute>
	{
		public ClientAppStrategy(IServicePublisher publisher) : base(publisher)
		{
		}

		/// <summary>
		/// Создать элемент.
		/// </summary>
		public override ClientApplicationInfo CreateElement(
			IExtensionAttachmentContext<Type, Attribute> context,
			ClientApplicationAttribute attr)
		{
			Type clCont = typeof (IClientApplication);
			if (!clCont.IsAssignableFrom(context.ExtensionType))
				throw new ArgumentException("Client application must implement '" + clCont.FullName
					+ "' interface.");
			return new ClientApplicationInfo(context.ExtensionType, attr.Name, attr.DisplayName,
				attr.Description);
		}
	}
}
