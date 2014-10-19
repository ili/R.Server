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
		/// ���������� ����������.
		/// </summary>
		protected override void Attach(ExtensionAttachmentContext context, SimpleExtensionAttribute attribute)
		{
			_writer.WriteLine(
				string.Format("{0}({1})",
				attribute.Name,
				context.Type.FullName));
		}
	}
}
