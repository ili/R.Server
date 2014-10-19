using System;
using System.ServiceModel.Channels;

using R.Server.ClientModel;

using Rsdn.SmartApp;

namespace R.Server.ClientCommon
{
	/// <summary>
	/// Communication helper
	/// </summary>
	public class CommHelper
	{
		private readonly ICommTransportManager _transportMgr;

		public CommHelper(IServiceProvider provider)
		{
			_transportMgr = provider.GetService<ICommTransportManager>();
			if (_transportMgr == null)
				throw new ServiceNotFoundException(typeof (ICommTransportManager));
		}

		/// <summary>
		/// Create endpoint URI.
		/// </summary>
		public Uri CreateEndpointUri(string transport, string host, int port, string path)
		{
			if (string.IsNullOrEmpty(transport))
				throw new ArgumentNullException("transport");
			if (!_transportMgr.IsTransportExists(transport))
				throw new ArgumentException("Transport '" + transport + "' not registered.");
			if (string.IsNullOrEmpty(host))
				throw new ArgumentNullException("host");
			if (port < 0 || port > 65535)
				throw new ArgumentOutOfRangeException("port");

			return new Uri(
				new Uri(_transportMgr.GetUriSchema(transport) + Uri.SchemeDelimiter + host
					+ (port != 0 ? ":" + port : "")),
				path);
		}

		public Binding CreateBinding(string transport)
		{
			if (!_transportMgr.IsTransportExists(transport))
				throw new ArgumentException("Transport '" + transport + "' not registered.");
			return new CustomBinding(
				SecurityBindingElement.CreateUserNameOverTransportBindingElement(),
				new WindowsStreamSecurityBindingElement(),
				_transportMgr.CreateTransportElement(transport));
		}
	}
}