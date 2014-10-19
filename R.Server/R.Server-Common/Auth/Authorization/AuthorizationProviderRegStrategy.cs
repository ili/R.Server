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
		/// ������� �������.
		/// </summary>
		public override AuthorizationProviderInfo CreateElement(
			IExtensionAttachmentContext<Type, Attribute> context,
			AuthorizationProviderAttribute attr)
		{
			if (!typeof (IAuthorizationProvider).IsAssignableFrom(context.ExtensionType))
				throw new ArgumentException("Authentication provider must implement '"
					+ typeof (IAuthorizationProvider).FullName + "' interface.");
			return new AuthorizationProviderInfo(attr.Name, context.ExtensionType);
		}
	}
}