using System;

using R.Server.Common.ObjectModel;

namespace R.Server.Common.ObjectModel
{
	/// <summary>
	/// ������ ���������. ����������� ������������� ��� ����������� ������
	/// </summary>
	public interface IMessageVersion : IContentObject
	{
		DateTime VersionDate { get; set; }

		string Text { get; set; }

		IMessage Message { get; set; }
	}
}