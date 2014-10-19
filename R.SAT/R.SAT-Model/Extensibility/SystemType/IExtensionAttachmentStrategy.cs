using System;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// Стратегия подключения расширения для <see cref="Type"/>.
	/// </summary>
	public interface IExtensionAttachmentStrategy : IExtensionAttachmentStrategy<Type, Attribute>
	{}
}