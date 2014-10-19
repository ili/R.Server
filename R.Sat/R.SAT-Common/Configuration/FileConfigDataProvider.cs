using System;
using System.IO;
using System.Xml;

namespace Rsdn.SmartApp.Configuration
{
	/// <summary>
	/// ��������� ������� ����������������, �������������� �������� ������������
	/// � ����� �� �����.
	/// </summary>
	public class FileConfigDataProvider : IConfigDataProvider, IDisposable
	{
		private readonly FileSystemWatcher _changeWatcher;
		private readonly string _fileName;
		private readonly string _dirName;
		private EventHandler<IConfigDataProvider> _configChanged;

		/// <summary>
		/// �������������� ���������.
		/// </summary>
		/// <param name="fileName">��� ����� ������������</param>
		public FileConfigDataProvider(string fileName)
		{
			if (string.IsNullOrEmpty(fileName))
				throw new ArgumentNullException("fileName");
			_dirName = Path.GetDirectoryName(fileName) ?? Path.PathSeparator.ToString();
			_fileName = fileName;
			var shortName = Path.GetFileName(fileName);
			if (shortName == null)
				throw new ArgumentException("Supplied path contains only directory");
			if (!File.Exists(_fileName))
				throw new FileNotFoundException("Configuration file not found", _fileName);
			_changeWatcher =
				new FileSystemWatcher(
					_dirName,
					shortName);
			_changeWatcher.Changed += ConfigFileChanged;
		}

		/// <summary>
		/// ��� ����� ������������.
		/// </summary>
		public string FileName
		{
			get { return _fileName; }
		}

		#region IConfigDataProvider Members
		/// <summary>
		/// ������ ������ ������������.
		/// </summary>
		/// <remarks>����� ����� ��������� ���������� �����</remarks>
		public XmlReader ReadData()
		{
			return XmlReader.Create(
				new FileStream(_fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
		}

		IConfigDataProvider IConfigDataProvider.ResolveInclude(string include)
		{
			return ResolveInclude(include);
		}

		/// <summary>
		/// ���������� ��� ��������� ����� ������������ �������� ����������.
		/// </summary>
		public virtual event EventHandler<IConfigDataProvider> ConfigChanged
		{
			add
			{
				_configChanged += value;
				_changeWatcher.EnableRaisingEvents = true;
			}
			remove { _configChanged -= value; }
		}
		#endregion

		#region IDisposable Members
		/// <summary>
		/// See <see cref="IDisposable.Dispose"/>
		/// </summary>
		public void Dispose()
		{
			_changeWatcher.Dispose();
		}
		#endregion

		private void ConfigFileChanged(object sender, FileSystemEventArgs e)
		{
			OnConfigChanged();
		}

		/// <summary>
		/// ��������� ���������.
		/// </summary>
		public FileConfigDataProvider ResolveInclude(string include)
		{
			var incFileName = Path.Combine(_dirName, include);
			if (!File.Exists(incFileName))
				throw new ArgumentException("Could not find file '" + incFileName + "'");
			return new FileConfigDataProvider(incFileName);
		}

		/// <summary>
		/// �������� ������� <see cref="ConfigChanged"/>
		/// </summary>
		protected virtual void OnConfigChanged()
		{
			if (_configChanged != null)
				_configChanged(this);
		}
	}
}