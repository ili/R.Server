namespace Rsdn.SmartApp
{
	/// <summary>
	/// ��������� ��������, ������������ ���������� �� ���������� ��� ����������.
	/// </summary>
	public interface IScanFilterAttribute<T, A>
	{
		/// <summary>
		/// ���������� true, ���� ���������� ��������� ����������� ����������.
		/// </summary>
		bool AllowAttach(IExtensionAttachmentContext<T, A> context, A attribute);
	}
}