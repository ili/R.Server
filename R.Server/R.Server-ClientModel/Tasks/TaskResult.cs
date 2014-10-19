using System.Runtime.Serialization;

namespace R.Server.ClientModel
{
	[DataContract]
	public class TaskResult
	{
		public TaskResult(bool isSuccess, string result, string errorMessage)
		{
			IsSuccess = isSuccess;
			Result = result;
			ErrorMessage = errorMessage;
		}

		[DataMember]
		public bool IsSuccess { get; set; }

		[DataMember]
		public string Result { get; set; }

		[DataMember]
		public string ErrorMessage { get; set; }
	}
}