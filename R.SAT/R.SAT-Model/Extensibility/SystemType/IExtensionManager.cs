using System;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// Интерфейс менеджера расширений.
	/// </summary>
	public interface IExtensionManager : IExtensionManager<Type, Attribute>
	{}
}