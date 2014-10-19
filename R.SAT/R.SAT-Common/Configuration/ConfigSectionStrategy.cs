using System;

namespace Rsdn.SmartApp.Configuration
{
	/// <summary>
	/// ��������� ����������� ������ ������������.
	/// </summary>
	public class ConfigSectionStrategy
		: RegKeyedElementsStrategy<Type, ConfigSectionInfo, ConfigSectionAttribute>
	{
		/// <summary>
		/// �������������� ���������.
		/// </summary>
		public ConfigSectionStrategy(IServicePublisher publisher)
			: base(publisher)
		{
		}

		/// <summary>
		/// ������� �������.
		/// </summary>
		public override ConfigSectionInfo CreateElement(
			IExtensionAttachmentContext<Type, Attribute> context,
			ConfigSectionAttribute attr)
		{
			return new ConfigSectionInfo(attr.Name, attr.Namespace, attr.AllowMerge,
				context.ExtensionType, attr.CreateSerializer(context.ExtensionType));
		}
	}
}