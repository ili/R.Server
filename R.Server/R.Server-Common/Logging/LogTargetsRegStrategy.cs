﻿using System;

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
			ExtensionAttachmentContext context,
			LogTargetAttribute attr)
		{
			if (!typeof (ILogTarget).IsAssignableFrom(context.Type))
				throw new ArgumentException("Log target must implement '" + typeof (ILogTarget).FullName
					+ "' interface.");
			return new LogTargetInfo(attr.Name, context.Type);
		}
	}
}