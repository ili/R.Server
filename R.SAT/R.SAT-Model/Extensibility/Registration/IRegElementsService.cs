namespace Rsdn.SmartApp
{
	/// <summary>
	/// Сервис регистрации элементов.
	/// </summary>
	public interface IRegElementsService<EI>
	{
		/// <summary>
		/// Зарегистрировать элемент.
		/// </summary>
		void Register(EI elementInfo);

		/// <summary>
		/// Получить список зарегистрированных элементов.
		/// </summary>
		EI[] GetRegisteredElements();
	}
}