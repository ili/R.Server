using System;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// ������� ���������� �������� ������������ ����������.
	/// </summary>
	public abstract class NamedElementInfo : KeyedElementInfo<string>
	{
		/// <summary>
		/// �������������� ���������.
		/// </summary>
		protected NamedElementInfo(string name, Type type)
			: base(type, name)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException("name");
		}

		/// <summary>
		/// ��� ����������.
		/// </summary>
		public string Name
		{
			get { return Key; }
		}
	}
}