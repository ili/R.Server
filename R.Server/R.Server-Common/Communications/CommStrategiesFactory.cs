using System;

using Rsdn.SmartApp;

namespace R.Server.Common
{
	[ExtensionStrategyFactory]
	internal class CommStrategiesFactory : IExtensionStrategyFactory
	{
		#region IExtensionStrategyFactory Members
		/// <summary>
		/// Создает стратегии.
		/// </summary>
		public IExtensionAttachmentStrategy[] CreateStrategies(IServiceProvider provider)
		{
			var publisher = provider.GetService<IServicePublisher>();
			return new IExtensionAttachmentStrategy[]
				{
					new CommServiceRegStrategy(publisher),
					new AuthenticationProviderRegStrategy(publisher),
					new AuthorizationProviderRegStrategy(publisher),
					new CommTransportRegStrategy(publisher)
				};
		}
		#endregion
	}
}