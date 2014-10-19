using System;
using System.Collections.Generic;
using System.IO;

using R.Server.ServerModel;

namespace R.Server.Common
{
	/// <summary>
	/// LogInfo target for console.
	/// </summary>
	[LogTarget(TargetName)]
	public class ConsoleLogTarget : TextWriterLogTarget
	{
		private static readonly Dictionary<LogEventType, ConsoleColor> _colorMap =
			new Dictionary<LogEventType, ConsoleColor>();

		static ConsoleLogTarget()
		{
			_colorMap.Add(LogEventType.Information, ConsoleColor.Green);
			_colorMap.Add(LogEventType.Warning, ConsoleColor.Yellow);
			_colorMap.Add(LogEventType.Error, ConsoleColor.DarkRed);
			_colorMap.Add(LogEventType.CriticalError, ConsoleColor.Red);
		}

		public const string TargetName = "Console";

		public ConsoleLogTarget(IServiceProvider provider) : base(provider)
		{
		}

		public override TextWriter CreateWriter()
		{
			return Console.Out;
		}

		public override void WriteEvent(LogEventInfo eventInfo, DateTime eventDate)
		{
			using (new ColorHelper(_colorMap[eventInfo.Type]))
				base.WriteEvent(eventInfo, eventDate);
		}

		#region ColorHelper class
		private class ColorHelper : IDisposable
		{
			private readonly ConsoleColor _oldColor;

			public ColorHelper(ConsoleColor color)
			{
				_oldColor = Console.ForegroundColor;
				Console.ForegroundColor = color;
			}

			#region IDisposable Members
			public void Dispose()
			{
				Console.ForegroundColor = _oldColor;
			}
			#endregion
		}
		#endregion
	}
}