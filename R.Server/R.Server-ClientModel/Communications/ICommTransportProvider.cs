using System.ServiceModel.Channels;

namespace R.Server.ServerModel
{
	/// <summary>
	/// Transport binding provider for WCF layer.
	/// </summary>
	public interface ICommTransportProvider
	{
		/// <summary>
		/// URI schema for transport.
		/// </summary>
		string Schema { get; }

		TransportBindingElement CreateTransportElement();
	}
}