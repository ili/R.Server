using System;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// ��������������� ����� ��� ���������� ��������� �����������.
	/// </summary>
	public abstract class RegKeyedElementsStrategy<K, EI, A>
		: RegElementsStrategy<EI, A>
		where A : Attribute
		where EI : class, IKeyedElementInfo<K> where K : class
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
		protected override void Attach(IExtensionAttachmentContext<Type, Attribute> context, A attribute)
		{
			base.Attach(context, attribute);
			CheckService();
		}

		/// <summary>
		/// ������� ���������� �������.
		/// </summary>
		protected override IRegElementsService<EI> CreateService(
			IExtensionAttachmentContext<Type, Attribute> context,
			A attr)
		{
			return new RegKeyedElementsService<K, EI>();
		}

		private void CheckService()
		{
			var regSvc =
				Publisher.GetService<IRegKeyedElementsService<K, EI>>();
			if (regSvc != null)
				return;
			regSvc = (IRegKeyedElementsService<K, EI>)Publisher.GetService<IRegElementsService<EI>>();
			Publisher.Publish(regSvc);
		}
	}
}
