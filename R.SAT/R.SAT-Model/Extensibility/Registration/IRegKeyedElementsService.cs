namespace Rsdn.SmartApp
{
	/// <summary>
	/// ������ ����������� ����������� ���������.
	/// </summary>
	public interface IRegKeyedElementsService<K, EI> : IRegElementsService<EI>
		where EI : IKeyedElementInfo<K>
	{
		/// <summary>
		/// ���������, ���� �� ������������������ ������� � ��������� ������.
		/// </summary>
		bool ContainsElement(K key);

		/// <summary>
		/// ���������� ������� �� ��� �����.
		/// </summary>
		EI GetElement(K name);
	}
}