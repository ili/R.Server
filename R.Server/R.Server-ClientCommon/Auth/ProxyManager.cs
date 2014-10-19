using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

using Rsdn.SmartApp;

namespace R.Server.ClientCommon
{
	public class ProxyManager
	{
		private RServerCredentials _credentials;
		private readonly Binding _binding;
		private readonly Uri _baseAddress;
		private readonly ElementsCache<Type, ChannelFactory> _factories;

		public ProxyManager(IServiceProvider provider, RServerCredentials credentials, string transport,
			string host, int port)
		{
			_credentials = credentials;
			var ch = new CommHelper(provider);
			_binding = ch.CreateBinding(transport);
			_baseAddress = ch.CreateEndpointUri(transport, host, port, "");
			_factories = new ElementsCache<Type, ChannelFactory>(CreateFactory);
		}

		public RServerCredentials Credentials
		{
			get { return _credentials; }
			set
			{
				_credentials = value;
				_factories.Reset();
			}
		}

		private ChannelFactory CreateFactory(Type type)
		{
			var cf = (ChannelFactory) Activator.CreateInstance(
				typeof (ChannelFactory<>).MakeGenericType(type), _binding);
			cf.Credentials.UserName.UserName = _credentials.ToString();
			cf.Credentials.UserName.Password = _credentials.Password;
			return cf;
		}

		public T CreateProxy<T>(string serviceName)
		{
			var ea = new EndpointAddress(new Uri(_baseAddress, serviceName));
			return ((ChannelFactory<T>) _factories.Get(typeof (T))).CreateChannel(ea);
		}
	}
}