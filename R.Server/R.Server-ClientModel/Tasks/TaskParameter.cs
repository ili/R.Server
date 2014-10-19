using System.Runtime.Serialization;

namespace R.Server.ClientModel
{
	[DataContract]
	public class TaskParameter
	{
		public TaskParameter(string name, string value)
		{
			Name = name;
			Value = value;
		}

		[DataMember]
		public string Name { get; set; }

		[DataMember]
		public string Value { get; set; }
	}
}