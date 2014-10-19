using System;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// Базовая generic-реализация стратегии для <see cref="Type"/>.
	/// </summary>
	public abstract class AttachmentStrategyBase<A> : AttachmentStrategyBase, IExtensionAttachmentStrategy
		where A : Attribute
	{
		/// <summary>
		/// Инициализирует экземпляр.
		/// </summary>
		protected AttachmentStrategyBase() : base(new [] {typeof (A)})
		{}

		/// <summary>
		/// Подключает расширение.
		/// </summary>
		protected abstract void Attach(
			IExtensionAttachmentContext<Type, Attribute> context,
			A attribute);

		/// <summary>
		/// Подключает расширение.
		/// </summary>
		protected override void Attach(IExtensionAttachmentContext<Type, Attribute> context, Attribute attribute)
		{
			Attach(context, (A)attribute);
		}
	}
}