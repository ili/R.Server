using System;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// Вспомогательный класс для реализации атрибутов регистрации.
	/// </summary>
	public abstract class RegElementsStrategy<EI, A> : AttachmentStrategyBase<A>
		where A : Attribute where EI : class
	{
		private readonly IServicePublisher _publisher;

		/// <summary>
		/// Инициализирует экземпляр.
		/// </summary>
		protected RegElementsStrategy(IServicePublisher publisher)
		{
			if (publisher == null)
				throw new ArgumentNullException("publisher");
			_publisher = publisher;
		}

		/// <summary>
		/// Публикатор сервисов.
		/// </summary>
		protected IServicePublisher Publisher
		{
			get { return _publisher; }
		}

		/// <summary>
		/// Подключает расширение.
		/// </summary>
		protected override void Attach(IExtensionAttachmentContext<Type, Attribute> context, A attribute)
		{
			var regSvc = GetService(context, attribute);
			regSvc.Register(CreateElement(context, attribute));
		}

		/// <summary>
		/// Создать элемент.
		/// </summary>
		public abstract EI CreateElement(IExtensionAttachmentContext<Type, Attribute> context, A attr);

		/// <summary>
		/// Создать реализацию сервиса.
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