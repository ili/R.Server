using System;

using R.Server.ServerModel;

using Rsdn.SmartApp;

namespace R.Server.Common
{
	public class TaskRegStrategy : RegKeyedElementsStrategy<string, TaskInfo, TaskAttribute>
	{
		public TaskRegStrategy(IServicePublisher publisher) : base(publisher)
		{
		}

		/// <summary>
		/// Создать элемент.
		/// </summary>
		public override TaskInfo CreateElement(
			IExtensionAttachmentContext<Type, Attribute> context,
			TaskAttribute attr)
		{
			return new TaskInfo(attr.Name, context.ExtensionType);
		}
	}
}