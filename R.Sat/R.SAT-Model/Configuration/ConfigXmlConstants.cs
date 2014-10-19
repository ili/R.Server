namespace Rsdn.SmartApp.Configuration
{
	/// <summary>
	/// ���������, ��������� � XML-�������������� ������������.
	/// </summary>
	public static class ConfigXmlConstants
	{
		/// <summary>
		/// XML-���������.
		/// </summary>
		public const string XmlNamespace =
			"http://rsdn.ru/R.SAT/ConfigSectionSchema.xsd";

		/// <summary>
		/// ��� ������� �� ������.
		/// </summary>
		public const string XmlSchemaResource =
			"Rsdn.SmartApp.Configuration.ConfigSectionSchema.xsd";

		/// <summary>
		/// ��� ���� � ��������.
		/// </summary>
		public const string IncludeTagName = "include";

		/// <summary>
		/// ��� ���� � ����������.
		/// </summary>
		public const string VariableTagName = "var";

		/// <summary>
		/// ��� �������� � ������ ����������.
		/// </summary>
		public const string VariableNameAttribute = "name";
	}
}