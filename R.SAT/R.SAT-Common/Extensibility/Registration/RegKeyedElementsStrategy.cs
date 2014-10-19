using System;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// Вспомогательный класс для реализации атрибутов регистрации.
	/// </summary>
	public abstract class RegKeyedElementsStrategy<K, EI, A>
		: RegElementsStrategy<EI, A>
		where A : Attribute
		where EI : class, IKeyedElementInfo<K> where K : class
	{
		/// <summary>
		/// Инициализирует экземпляр.
		/// </summary>
		protected RegKeyedElementsStrategy(IServicePublisher publisher)
			: base(publisher)
		{ }

		/// <summary>
		/// Подключает расширение.
		/// </summary>
		protected override void Attach(IExtensionAttachmentContext<Type, Attribute> context, A attribute)
		{
			base.Attach(context, attribute);
			CheckService();
		}

		/// <summary>
		/// Создать реализацию сервиса.
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
