using System;

using JetBrains.Annotations;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// ��������� ������, ���������������� ������������� ���������.
	/// </summary>
	public interface IExtensionStrategyFactory
	{
		/// <summary>
		/// ������� ���������.
		/// </summary>
		[NotNull]
		IExtensionAttachmentStrategy[] CreateStrategies([NotNull] IServiceProvider provider);
	}
}