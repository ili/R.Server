using System.Runtime.Serialization;

namespace R.Server.ClientModel
{
	[DataContract]
	public class RServerInfo
	{
		public RServerInfo(string name, string description)
		{
			Name = name;
			Description = description;
		}

		[DataMember]
		public string Name { get; set; }

		[DataMember]
		public string Description { get; set; }
	}
}