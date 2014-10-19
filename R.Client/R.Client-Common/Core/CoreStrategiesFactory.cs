using System;
using Rsdn.SmartApp;
using Rsdn.SmartApp.Configuration;

namespace R.Client.Common
{
	[ExtensionStrategyFactory]
	internal class CoreStrategiesFactory : IExtensionStrategyFactory
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
				new ConfigSectionStrategy(publisher),
				new ClientAppStrategy(publisher)
			};
		}
		#endregion
	}
}
