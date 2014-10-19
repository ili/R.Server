namespace Rsdn.SmartApp
{
	/// <summary>
	/// Обработчик события без аргументов.
	/// </summary>
	public delegate void EventHandler<S>(S sender);

	/// <summary>
	/// Обработчик события с аргументами.
	/// </summary>
	public delegate void EventHandler<S, P>(S sender, P eventParams);
}