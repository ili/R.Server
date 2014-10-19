using System;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// ��������������� ����� ��� ���������� ��������� �����������.
	/// </summary>
	public abstract class RegKeyedElementsStrategy<TKey, TInfo, TAttribute>
		: RegElementsStrategy<TInfo, TAttribute>
		where TAttribute : Attribute
		where TInfo : class, IKeyedElementInfo<TKey> where TKey : class
	{
		/// <summary>
		/// �������������� ���������.
		/// </summary>
		protected RegKeyedElementsStrategy(IServicePublisher publisher)
			: base(publisher)
		{ }

		/// <summary>
		/// ���������� ����������.
		/// </summary>
		protected override void Attach(ExtensionAttachmentContext context, TAttribute attribute)
		{
			base.Attach(context, attribute);
			CheckService();
		}

		/// <summary>
		/// ������� ���������� �������.
		/// </summary>
		protected override IRegElementsService<TInfo> CreateService(ExtensionAttachmentContext context, TAttribute attr)
		{
			return new RegKeyedElementsService<TKey, TInfo>();
		}

		private void CheckService()
		{
			var regSvc =
				Publisher.GetService<IRegKeyedElementsService<TKey, TInfo>>();
			if (regSvc != null)
				return;
			regSvc = (IRegKeyedElementsService<TKey, TInfo>)Publisher.GetRequiredService<IRegElementsService<TInfo>>();
			Publisher.Publish(regSvc);
		}
	}
}
