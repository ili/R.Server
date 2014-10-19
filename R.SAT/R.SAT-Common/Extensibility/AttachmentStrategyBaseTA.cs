namespace Rsdn.SmartApp
{
	/// <summary>
	/// ������� ���������� ���������.
	/// </summary>
	public abstract class AttachmentStrategyBase<T, A> : IExtensionAttachmentStrategy<T, A>
	{
		/// <summary>
		/// ���������� true, ���� ������� ������������� ���������.
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
		/// ���������� ����������.
		/// </summary>
		protected abstract void Attach(
			IExtensionAttachmentContext<T, A> context,
			A attribute);
	}
}