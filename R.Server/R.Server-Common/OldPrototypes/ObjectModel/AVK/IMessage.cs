using R.Server.Common.ObjectModel;

namespace R.Server.Common.ObjectModel
{
	/// <summary>
	/// ���������
	/// </summary>
	public interface IMessage : IContentObject
	{
		/// <summary>
		/// ������ �� �����
		/// </summary>
		ITopic Topic { get; set; }

		/// <summary>
		/// ����
		/// </summary>
		string Subject { get; set; }

		/// <summary>
		/// �����
		/// </summary>
		string Text { get; set; }

		string Name { get; set; }
	}
}