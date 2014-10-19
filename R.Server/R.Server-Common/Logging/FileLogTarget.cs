#pragma warning disable 1692

using System;
using System.Collections.Generic;
using System.IO;

using R.Server.ServerModel;

using Rsdn.SmartApp;

namespace R.Server.Common
{
	[LogTarget(TargetName)]
	public class FileLogTarget : TextWriterLogTarget
	{
		public const string TargetName = "File";
		public const string PathParamName = "path";

		private readonly IDictionary<string, TargetParameter> _parameters;
		private readonly string _path;

		[ExpectService]
		private IRServer _rServer;

		public FileLogTarget(IServiceProvider provider,
			ILogger logger,
#pragma warning disable SuggestBaseTypeForParameter
			TargetParameter[] targetParams) : base(provider)
#pragma warning restore SuggestBaseTypeForParameter
		{
			using (var writer = CreateWriter())
				writer.WriteLine("   ---   New server session started at {0}", DateTime.Now);

			_parameters = targetParams.Convert2Dictionary(prm => prm.Name);
			if (!_parameters.ContainsKey(PathParamName))
			{
				logger.LogEvent(new LogEventInfo(
					"Required parameter 'path' not specified. Use server working directory.",
					"FileLogTarget",
					LogEventImpotance.Important,
					LogEventType.Warning));
				_path = _rServer.WorkingDirectory;
			}
			else
			{
				_path = _parameters[PathParamName].Value;
				if (!Path.IsPathRooted(_path))
					_path = Path.Combine(_rServer.WorkingDirectory, _path ?? "");
			}
			if (!Directory.Exists(_path))
				throw new ArgumentException("Directory '" + _path + "' not exists.");
		}

		public override TextWriter CreateWriter()
		{
			var now = DateTime.Now;
			var fileName = string.Format("{0}_{1}-{2}-{3}.log", _rServer.Name, now.Year, now.Month, now.Day);
			return
				new StreamWriter(new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.Read));
		}
	}
}