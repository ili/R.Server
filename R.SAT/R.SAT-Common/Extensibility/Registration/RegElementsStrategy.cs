using System;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// ��������������� ����� ��� ���������� ��������� �����������.
	/// </summary>
	public abstract class RegElementsStrategy<EI, A> : AttachmentStrategyBase<A>
		where A : Attribute where EI : class
	{
		private readonly IServicePublisher _publisher;

		/// <summary>
		/// �������������� ���������.
		/// </summary>
		protected RegElementsStrategy(IServicePublisher publisher)
		{
			if (publisher == null)
				throw new ArgumentNullException("publisher");
			_publisher = publisher;
		}

		/// <summary>
		/// ���������� ��������.
		/// </summary>
		protected IServicePublisher Publisher
		{
			get { return _publisher; }
		}

		/// <summary>
		/// ���������� ����������.
		/// </summary>
		protected override void Attach(IExtensionAttachmentContext<Type, Attribute> context, A attribute)
		{
			var regSvc = GetService(context, attribute);
			regSvc.Register(CreateElement(context, attribute));
		}

		/// <summary>
		/// ������� �������.
		/// </summary>
		public abstract EI CreateElement(IExtensionAttachmentContext<Type, Attribute> context, A attr);

		/// <summary>
		/// ������� ���������� �������.
		/// </summary>
		protected virtual IRegElementsService<EI> CreateService(
			IExtensionAttachmentContext<Type, Attribute> context,
			A attr)
		{
			return new RegElementsService<EI>();
		}

		private IRegElementsService<EI> GetService(
			IExtensionAttachmentContext<Type, Attribute> context,
			A attr)
		{
			var regSvc = Publisher.GetService<IRegElementsService<EI>>();
			if (regSvc == null)
			{
				regSvc = CreateService(context, attr);
				Publisher.Publish(regSvc);
			}
			return regSvc;
		}
	}
}