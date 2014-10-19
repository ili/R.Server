using System;

namespace Rsdn.SmartApp.Configuration
{
	/// <summary>
	/// �������� ������ ������������.
	/// </summary>
	public class ConfigSectionInfo : IKeyedElementInfo<Type>
	{
		/// <summary>
		/// �������������� ���������.
		/// </summary>
		public ConfigSectionInfo(string name, string ns, bool allowMerge, Type contractType,
			IConfigSectionSerializer serializer)
		{
			Name = name;
			Namespace = ns;
			AllowMerge = allowMerge;
			ContractType = contractType;
			Serializer = serializer;
		}

		/// <summary>
		/// ��� ������.
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// XML_������������ ����.
		/// </summary>
		public string Namespace { get; private set; }

		/// <summary>
		/// ��������� �� ������� ������ � ������ ������.
		/// </summary>
		public bool AllowMerge { get; private set; }

		/// <summary>
		/// ��� ��������� ������.
		/// </summary>
		public Type ContractType { get; private set; }

		/// <summary>
		/// ������������.
		/// </summary>
		public IConfigSectionSerializer Serializer { get; private set; }

		/// <summary>
		/// ���.
		/// </summary>
		Type IKeyedElementInfo<Type>.Key
		{
			get { return ContractType; }
		}
	}
}