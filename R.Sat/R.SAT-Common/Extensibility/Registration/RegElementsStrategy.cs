using System;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// ��������������� ����� ��� ���������� ��������� �����������.
	/// </summary>
	public abstract class RegElementsStrategy<TInfo, TAttribute> : AttachmentStrategyBase<TAttribute>
		where TAttribute : Attribute where TInfo : class
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
		protected override void Attach(ExtensionAttachmentContext context, TAttribute attribute)
		{
			var regSvc = GetService(context, attribute);
			regSvc.Register(CreateElement(context, attribute));
		}

		/// <summary>
		/// ������� �������.
		/// </summary>
		public abstract TInfo CreateElement(ExtensionAttachmentContext context, TAttribute attr);

		/// <summary>
		/// ������� ���������� �������.
		/// </summary>
		protected virtual IRegElementsService<TInfo> CreateService(ExtensionAttachmentContext context, TAttribute attr)
		{
			return new RegElementsService<TInfo>();
		}

		private IRegElementsService<TInfo> GetService(ExtensionAttachmentContext context, TAttribute attr)
		{
			var regSvc = Publisher.GetService<IRegElementsService<TInfo>>();
			if (regSvc == null)
			{
				regSvc = CreateService(context, attr);
				Publisher.Publish(regSvc);
			}
			return regSvc;
		}
	}
}