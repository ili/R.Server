using System.ServiceModel.Channels;

namespace R.Server.ClientModel
{
	/// <summary>
	/// Transport bindings manager
	/// </summary>
	public interface ICommTransportManager
	{
		bool IsTransportExists(string name);

		string GetUriSchema(string name);

		TransportBindingElement CreateTransportElement(string name);
	}
}