using System;
using System.Reflection;

using R.Client.Model;
using R.Common;

using IServiceProvider=Rsdn.SmartApp.IServiceProvider;

namespace R.Client.WpfHost.About
{
	internal class AboutWindowModel
	{
		private readonly IServiceProvider _provider;

		public AboutWindowModel(IServiceProvider provider)
		{
			_provider = provider;
		}

		public string ProductName
		{
			get { return CommonConstants.ProductName; }
		}

		public string AppName
		{
			get { return Constants.ApplicationName; }
		}

		public Version AppVersion
		{
			get { return GetType().Assembly.GetName().Version; }
		}

		public string Copyright
		{
			get { return CommonConstants.CopyrightString; }
		}

		public Assembly[] LoadedExtensions
		{
			get { return _provider.GetService<IClientHost>().LoadedExtensions; }
		}
	}
}
