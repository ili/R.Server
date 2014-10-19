using System;
using System.IO;

namespace Rsdn.SmartApp.Demos
{
	internal class SimpleExtensionStrategy
		: AttachmentStrategyBase<SimpleExtensionAttribute>
	{
		private readonly TextWriter _writer;

		public SimpleExtensionStrategy(TextWriter writer)
		{
			_writer = writer;
		}

		/// <summary>
		/// Подключает расширение.
		/// </summary>
		protected override void Attach(
			IExtensionAttachmentContext<Type, Attribute> context,
			SimpleExtensionAttribute attribute)
		{
			_writer.WriteLine(string.Format("{0}({1})", attribute.Name,
				context.ExtensionType.FullName));
		}
	}
}
