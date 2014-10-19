﻿using System;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// Стратегия регистрации active parts.
	/// </summary>
	public class ActivePartRegStrategy
		: RegElementsStrategy<ActivePartInfo, ActivePartAttribute>
	{
		/// <include file='../CommonXmlDocs.xml' path='common-docs/def-ctor/*'/>
		public ActivePartRegStrategy(IServicePublisher publisher) : base(publisher)
		{}

		/// <summary>
		/// Создать описатель.
		/// </summary>
		public override ActivePartInfo CreateElement(
			IExtensionAttachmentContext<Type, Attribute> context,
			ActivePartAttribute attr)
		{
			var contract = typeof (IActivePart);
			if (!contract.IsAssignableFrom(context.ExtensionType))
				throw new ExtensibilityException(
					"Type '{0}' must implement interface '{1}".FormatStr(context.ExtensionType, contract));
			return new ActivePartInfo(context.ExtensionType);
		}
	}
}