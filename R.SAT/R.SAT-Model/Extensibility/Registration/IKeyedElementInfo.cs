namespace Rsdn.SmartApp
{
	/// <summary>
	/// ��������� ������������ ��������.
	/// </summary>
	public interface IKeyedElementInfo<K>
	{
		/// <summary>
		/// ����.
		/// </summary>
		K Key { get; }
	}
}