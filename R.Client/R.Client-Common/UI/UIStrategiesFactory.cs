using System;

using Rsdn.SmartApp;

namespace R.Client.Common
{
	[ExtensionStrategyFactory]
	public class UIStrategiesFactory : IExtensionStrategyFactory
	{
		#region IExtensionStrategyFactory Members
		/// <summary>
		/// Создает стратегии.
		/// </summary>
		public IExtensionAttachmentStrategy[] CreateStrategies(IServiceProvider provider)
		{
			var publisher = provider.GetService<IServicePublisher>();
			if (publisher == null)
				throw new ServiceNotFoundException(typeof (IServicePublisher));
			return new IExtensionAttachmentStrategy[]
			{
				new UIControllerRegStrategy(publisher),
				new ViewRegStrategy(publisher)
			};
		}
		#endregion
	}
}
