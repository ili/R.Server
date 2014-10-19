using System;
using System.Collections.Generic;
using System.IO;

using R.Server.ServerModel;

using Rsdn.SmartApp;

namespace R.Server.Common
{
	/// <summary>
	/// Write log info to <see cref="TextWriter"/>
	/// </summary>
	public abstract class TextWriterLogTarget : ServiceConsumer, ILogTarget
	{
		private static readonly Dictionary<LogEventType, char> _eventTypeChars =
			new Dictionary<LogEventType, char>();

		static TextWriterLogTarget()
		{
			_eventTypeChars.Add(LogEventType.Information, '#');
			_eventTypeChars.Add(LogEventType.Warning, '!');
			_eventTypeChars.Add(LogEventType.Error, 'X');
			_eventTypeChars.Add(LogEventType.CriticalError, '*');
		}

		protected TextWriterLogTarget(IServiceProvider provider) : base(provider)
		{
		}

		public abstract TextWriter CreateWriter();

		#region ILogTarget Members
		/// <summary>
		/// Write event to target
		/// </summary>
		public virtual void WriteEvent(LogEventInfo eventInfo, DateTime eventDate)
		{
			using (var writer = CreateWriter())
				writer.WriteLine("{0}[{1}][{2}] {3}", _eventTypeChars[eventInfo.Type], eventDate,
					eventInfo.Source, eventInfo.Message);
		}
		#endregion
	}
}