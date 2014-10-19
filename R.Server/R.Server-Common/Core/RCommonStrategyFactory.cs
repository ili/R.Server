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
			if(provider == null)
				throw new ArgumentNullException("provider");

			var publisher = provider.GetService<IServicePublisher>();
			if (publisher == null)
				throw new InvalidOperationException("Service publisher is not registered");

			return
				new IExtensionAttachmentStrategy[]
				{
					new LogTargetsRegStrategy(publisher),
					new TaskRegStrategy(publisher),
					new DBDriverRegStrategy(publisher),
					ServicesHelper.CreateServiceStrategy(publisher),
					new RegSecurityPolicyStrategy(publisher)
				};
		}
		#endregion
	}
}