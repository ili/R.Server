using System.ServiceModel.Channels;

using R.Server.ServerModel;

namespace R.Server.Common
{
	[CommTransportProvider(ProviderName)]
	public class HttpTransportProvider : ICommTransportProvider
	{
		public const string ProviderName = "http";

		#region ICommTransportProvider Members
		/// <summary>
		/// URI schema for transport.
		/// </summary>
		public string Schema
		{
			get { return "http"; }
		}

		public TransportBindingElement CreateTransportElement()
		{
			return new HttpTransportBindingElement();
		}
		#endregion
	}
}