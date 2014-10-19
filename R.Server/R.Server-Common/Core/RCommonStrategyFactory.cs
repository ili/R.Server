using System;

using Rsdn.SmartApp;

namespace R.Server.Common
{
	[ExtensionStrategyFactory]
	public class RCommonStrategyFactory : IExtensionStrategyFactory
	{
		#region IExtensionStrategyFactory Members
		/// <summary>
		/// Создает стратегии.
		/// </summary>
		public IExtensionAttachmentStrategy[] CreateStrategies(IServiceProvider provider)
		{
			var publisher = provider.GetService<IServicePublisher>();
			return
				new IExtensionAttachmentStrategy[]
				{
					new LogTargetsRegStrategy(publisher),
					new TaskRegStrategy(publisher),
					new DBDriverRegStrategy(publisher),
					new ServicePublishingStrategy(publisher),
					new RegSecurityPolicyStrategy(publisher)
				};
		}
		#endregion
	}
}