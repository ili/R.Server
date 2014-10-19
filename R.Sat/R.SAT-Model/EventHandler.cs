namespace Rsdn.SmartApp
{
	/// <summary>
	/// ���������� ������� ��� ����������.
	/// </summary>
	public delegate void EventHandler<TSender>(TSender sender);

	/// <summary>
	/// ���������� ������� � �����������.
	/// </summary>
	public delegate void EventHandler<TSender, TParams>(TSender sender, TParams eventParams);
}