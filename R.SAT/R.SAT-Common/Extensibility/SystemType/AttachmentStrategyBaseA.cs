using System;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// ������� generic-���������� ��������� ��� <see cref="Type"/>.
	/// </summary>
	public abstract class AttachmentStrategyBase<A> : AttachmentStrategyBase, IExtensionAttachmentStrategy
		where A : Attribute
	{
		/// <summary>
		/// �������������� ���������.
		/// </summary>
		protected AttachmentStrategyBase() : base(new [] {typeof (A)})
		{}

		/// <summary>
		/// ���������� ����������.
		/// </summary>
		protected abstract void Attach(
			IExtensionAttachmentContext<Type, Attribute> context,
			A attribute);

		/// <summary>
		/// ���������� ����������.
		/// </summary>
		protected override void Attach(IExtensionAttachmentContext<Type, Attribute> context, Attribute attribute)
		{
			Attach(context, (A)attribute);
		}
	}
}