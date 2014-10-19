using System;

using R.Server.ServerModel;

using Rsdn.SmartApp;

namespace R.Server.Common
{
	public class AuthorizationProviderRegStrategy
		: RegKeyedElementsStrategy<string, AuthorizationProviderInfo, AuthorizationProviderAttribute>
	{
		public AuthorizationProviderRegStrategy(IServicePublisher publisher) : base(publisher)
		{
		}

		/// <summary>
		/// Создать элемент.
		/// </summary>
		public override AuthorizationProviderInfo CreateElement(
			ExtensionAttachmentContext context,
			AuthorizationProviderAttribute attr)
		{
			if (!typeof (IAuthorizationProvider).IsAssignableFrom(context.Type))
				throw new ArgumentException("Authentication provider must implement '"
					+ typeof (IAuthorizationProvider).FullName + "' interface.");
			return new AuthorizationProviderInfo(attr.Name, context.Type);
		}
	}
}