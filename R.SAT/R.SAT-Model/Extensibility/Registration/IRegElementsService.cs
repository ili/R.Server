namespace Rsdn.SmartApp
{
	/// <summary>
	/// ������ ����������� ���������.
	/// </summary>
	public interface IRegElementsService<EI>
	{
		/// <summary>
		/// ���������������� �������.
		/// </summary>
		void Register(EI elementInfo);

		/// <summary>
		/// �������� ������ ������������������ ���������.
		/// </summary>
		EI[] GetRegisteredElements();
	}
}