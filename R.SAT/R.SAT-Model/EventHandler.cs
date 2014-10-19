namespace Rsdn.SmartApp
{
	/// <summary>
	/// ���������� ������� ��� ����������.
	/// </summary>
	public delegate void EventHandler<S>(S sender);

	/// <summary>
	/// ���������� ������� � �����������.
	/// </summary>
	public delegate void EventHandler<S, P>(S sender, P eventParams);
}