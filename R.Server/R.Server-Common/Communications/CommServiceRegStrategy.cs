using System;

using R.Server.ServerModel;

using Rsdn.SmartApp;

namespace R.Server.Common
{
	public class CommServiceRegStrategy
		: RegKeyedElementsStrategy<string, CommServiceInfo, CommServiceAttribute>
	{
		public CommServiceRegStrategy(IServicePublisher publisher) : base(publisher)
		{
		}

		/// <summary>
		/// Создать элемент.
		/// </summary>
		public override CommServiceInfo CreateElement(
			IExtensionAttachmentContext<Type, Attribute> context,
			CommServiceAttribute attr)
		{
			return new CommServiceInfo(attr.Name, context.ExtensionType, attr.ContractType);
		}
	}
}