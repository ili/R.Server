using System;

using R.Server.ServerModel;

using Rsdn.SmartApp;

namespace R.Server.Common
{
	public class AuthenticationProviderRegStrategy
		: RegKeyedElementsStrategy<string, AuthenticationProviderInfo, AuthenticationProviderAttribute>
	{
		public AuthenticationProviderRegStrategy(IServicePublisher publisher) : base(publisher)
		{
		}

		/// <summary>
		/// Создать элемент.
		/// </summary>
		public override AuthenticationProviderInfo CreateElement(
			ExtensionAttachmentContext context,
			AuthenticationProviderAttribute attr)
		{
			if (!typeof (IAuthenticationProvider).IsAssignableFrom(context.Type))
				throw new ArgumentException("Authentication provider must implement '"
					+ typeof (IAuthenticationProvider).FullName + "' interface.");
			return new AuthenticationProviderInfo(attr.Name, context.Type);
		}
	}
}