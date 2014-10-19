using System.ServiceModel.Channels;

using R.Server.ServerModel;

namespace R.Server.Common
{
	[CommTransportProvider(ProviderName)]
	public class NamedPipeTransportProvider : ICommTransportProvider
	{
		public const string ProviderName = "named-pipe";

		#region ICommTransportProvider Members
		/// <summary>
		/// URI schema for transport.
		/// </summary>
		public string Schema
		{
			get { return "net.pipe"; }
		}

		public TransportBindingElement CreateTransportElement()
		{
			return new NamedPipeTransportBindingElement();
		}
		#endregion
	}
}