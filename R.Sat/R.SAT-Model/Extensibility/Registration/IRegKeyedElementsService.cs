namespace Rsdn.SmartApp
{
	/// <summary>
	/// ������ ����������� ����������� ���������.
	/// </summary>
	public interface IRegKeyedElementsService<TKey, TInfo> : IRegElementsService<TInfo>
		where TInfo : IKeyedElementInfo<TKey>
	{
		/// <summary>
		/// ���������, ���� �� ������������������ ������� � ��������� ������.
		/// </summary>
		bool ContainsElement(TKey key);

		/// <summary>
		/// ���������� ������� �� ��� �����.
		/// </summary>
		TInfo GetElement(TKey key);
	}
}