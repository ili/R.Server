using System;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// Запрещает сканирование расширений с любым типеом параметров.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
	public class DisableAllScanAttribute : Attribute
	{
	}
}