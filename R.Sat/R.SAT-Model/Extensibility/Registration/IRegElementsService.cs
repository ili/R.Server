namespace Rsdn.SmartApp
{
	/// <summary>
	/// ������ ����������� ���������.
	/// </summary>
	public interface IRegElementsService<TInfo>
	{
		/// <summary>
		/// ���������������� �������.
		/// </summary>
		void Register(TInfo elementInfo);

		/// <summary>
		/// �������� ������ ������������������ ���������.
		/// </summary>
		TInfo[] GetRegisteredElements();
	}
}