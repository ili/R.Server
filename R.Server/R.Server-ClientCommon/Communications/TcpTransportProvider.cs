using System.ServiceModel.Channels;

using R.Server.ServerModel;

namespace R.Server.Common
{
	[CommTransportProvider(ProviderName)]
	public class TcpTransportProvider : ICommTransportProvider
	{
		public const string ProviderName = "tcp";

		#region ICommTransportProvider Members
		/// <summary>
		/// URI schema for transport.
		/// </summary>
		public string Schema
		{
			get { return "net.tcp"; }
		}

		public TransportBindingElement CreateTransportElement()
		{
			return new TcpTransportBindingElement();
		}
		#endregion
	}
}