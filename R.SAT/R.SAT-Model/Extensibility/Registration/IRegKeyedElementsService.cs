namespace Rsdn.SmartApp
{
	/// <summary>
	/// Сервис регистрации именованных элементов.
	/// </summary>
	public interface IRegKeyedElementsService<K, EI> : IRegElementsService<EI>
		where EI : IKeyedElementInfo<K>
	{
		/// <summary>
		/// Проверяет, есть ли зарегистрированный элемент с указанным именем.
		/// </summary>
		bool ContainsElement(K key);

		/// <summary>
		/// Возвращает элемент по его имени.
		/// </summary>
		EI GetElement(K name);
	}
}