using System;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// ��������� ������������ ���������� � ����� ������ ����������.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
	public class DisableAllScanAttribute : Attribute
	{
	}
}