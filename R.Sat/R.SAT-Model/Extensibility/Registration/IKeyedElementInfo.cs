namespace Rsdn.SmartApp
{
	/// <summary>
	/// ��������� ������������ ��������.
	/// </summary>
	public interface IKeyedElementInfo<TKey>
	{
		/// <summary>
		/// ����.
		/// </summary>
		TKey Key { get; }
	}
}