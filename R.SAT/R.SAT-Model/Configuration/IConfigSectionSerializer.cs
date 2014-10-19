using System.Xml;

namespace Rsdn.SmartApp.Configuration
{
	/// <summary>
	/// ������������ ������ ������������.
	/// </summary>
	public interface IConfigSectionSerializer
	{
		/// <summary>
		/// ��������������� ������.
		/// </summary>
		object Deserialize(XmlReader reader);

		/// <summary>
		/// ������� ������ �� ���������.
		/// </summary>
		object CreateDefaultSection();
	}
}