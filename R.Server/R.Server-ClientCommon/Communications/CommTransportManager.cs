using System;
using System.ServiceModel.Channels;

using R.Server.ClientModel;
using R.Server.ServerModel;

using Rsdn.SmartApp;

namespace R.Server.ClientCommon
{
	using ITransportSvc = IRegKeyedElementsService<string, CommTransportProviderInfo>;

	/// <summary>
	/// <see cref="ICommTransportManager"/> implementation.
	/// </summary>
	[Service(typeof (ICommTransportManager))]
	public class CommTransportManager : ICommTransportManager
	{
		private readonly ITransportSvc _transportSvc;
		private readonly ElementsCache<string, ICommTransportProvider> _transportProviders;

		public CommTransportManager(IServiceProvider provider)
		{
			_transportSvc = provider.GetService<ITransportSvc>();
			if (_transportSvc == null)
				throw new ServiceNotFoundException(typeof (ITransportSvc));
			_transportProviders = new ElementsCache<string, ICommTransportProvider>(
				delegate(string name)
					{
						var tInfo = _transportSvc.GetElement(name);
						if (tInfo == null)
							throw new ApplicationException("Could not find '" + name + "' transport.");
						return (ICommTransportProvider) tInfo.Type.CreateInstance(provider);
					});
		}

		#region ICommTransportManager Members
		public bool IsTransportExists(string name)
		{
			return _transportSvc.ContainsElement(name);
		}

		public string GetUriSchema(string name)
		{
			return _transportProviders.Get(name).Schema;
		}

		public TransportBindingElement CreateTransportElement(string name)
		{
			return _transportProviders.Get(name).CreateTransportElement();
		}
		#endregion
	}
}