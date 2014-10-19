using System;

using R.Server.ServerModel;

using Rsdn.SmartApp;

namespace R.Server.Common
{
	public class DBDriverRegStrategy
		: RegKeyedElementsStrategy<string, DBDriverInfo, DBDriverAttribute>
	{
		public DBDriverRegStrategy(IServicePublisher publisher) : base(publisher)
		{
		}

		/// <summary>
		/// Создать элемент.
		/// </summary>
		public override DBDriverInfo CreateElement(
			IExtensionAttachmentContext<Type, Attribute> context,
			DBDriverAttribute attr)
		{
			return new DBDriverInfo(attr.Name, context.ExtensionType);
		}
	}
}