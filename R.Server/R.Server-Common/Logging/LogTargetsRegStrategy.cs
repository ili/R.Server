using System;

using R.Server.ServerModel;

using Rsdn.SmartApp;

namespace R.Server.Common
{
	public class LogTargetsRegStrategy
		: RegKeyedElementsStrategy<string, LogTargetInfo, LogTargetAttribute>
	{
		public LogTargetsRegStrategy(IServicePublisher publisher) : base(publisher)
		{
		}

		/// <summary>
		/// Создать элемент.
		/// </summary>
		public override LogTargetInfo CreateElement(
			IExtensionAttachmentContext<Type, Attribute> context,
			LogTargetAttribute attr)
		{
			if (!typeof (ILogTarget).IsAssignableFrom(context.ExtensionType))
				throw new ArgumentException("Log target must implement '" + typeof (ILogTarget).FullName
					+ "' interface.");
			return new LogTargetInfo(attr.Name, context.ExtensionType);
		}
	}
}