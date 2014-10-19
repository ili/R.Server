namespace Rsdn.SmartApp
{
	/// <summary>
	/// Интерфейс именованного элемента.
	/// </summary>
	public interface IKeyedElementInfo<K>
	{
		/// <summary>
		/// Ключ.
		/// </summary>
		K Key { get; }
	}
}