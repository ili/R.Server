namespace Rsdn.SmartApp
{
	/// <summary>
	/// Базовая реализация стратегии.
	/// </summary>
	public abstract class AttachmentStrategyBase<T, A> : IExtensionAttachmentStrategy<T, A>
	{
		/// <summary>
		/// Возвращает true, если атрибут соответствует стратегии.
		/// </summary>
		protected abstract bool IsSuitable(A attribute);

		#region IExtensionAttachmentStrategy Members
		void IExtensionAttachmentStrategy<T, A>.Attach(
			IExtensionAttachmentContext<T, A> context,
			A attribute)
		{
			if (IsSuitable(attribute))
				Attach(context, attribute);
		}
		#endregion

		/// <summary>
		/// Подключает расширение.
		/// </summary>
		protected abstract void Attach(
			IExtensionAttachmentContext<T, A> context,
			A attribute);
	}
}