﻿using System;
using System.Globalization;
using System.IO;
using System.Reactive.Disposables;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// Base class for generators generated by RazorGenerator (http://razorgenerator.codeplex.com/).
	/// </summary>
	public abstract class RazorGenAppResource : TextAppResource
	{
		private TextWriter _writer;

		/// <summary>
		/// Initialize instance.
		/// </summary>
		protected RazorGenAppResource(IServiceProvider svcProvider) : base(svcProvider)
		{}

		/// <summary>
		/// Required by RazorGenerator.
		/// </summary>
		public abstract void Execute();

		protected IDisposable WriterGuard(TextWriter writer)
		{
			var oldWriter = _writer;
			_writer = writer;
			return Disposable.Create(() => _writer = oldWriter);
		}

		/// <summary>
		/// Write text to output.
		/// </summary>
		protected override void WriteText(AppResourceRequest request, TextWriter writer)
		{
			using (WriterGuard(writer))
				Execute();
		}

		/// <summary>
		/// Write string literal to output.
		/// </summary>
		protected void WriteLiteral(string literal)
		{
			_writer.Write(literal);
		}

		/// <summary>
		/// Write object to output.
		/// </summary>
		protected void Write(object value)
		{
			if ((value == null))
				return;

			WriteLiteral(Convert.ToString(value, CultureInfo.InvariantCulture));
		}
	}
}