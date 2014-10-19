using System;

namespace Rsdn.SmartApp.Configuration
{
	/// <summary>
	/// �������, ����������� ������ ������������.
	/// </summary>
	public abstract class ConfigSectionAttribute : Attribute
	{
		/// <summary>
		/// �������������� ���������. 
		/// </summary>
		protected ConfigSectionAttribute(string name, string ns)
		{
			Name = name;
			Namespace = ns;
			AllowMerge = false;
		}

		/// <summary>
		/// ��� ������.
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// ������������ ����.
		/// </summary>
		public string Namespace { get; private set; }

		/// <summary>
		/// ������� ������������ ������� ������.
		/// </summary>
		public bool AllowMerge { get; set; }

		/// <summary>
		/// ������� �����������.
		/// </summary>
		public abstract IConfigSectionSerializer CreateSerializer(Type contractType);
	}
}