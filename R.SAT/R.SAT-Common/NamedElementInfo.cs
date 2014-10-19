using System;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// ������� ���������� �������� ������������ ����������.
	/// </summary>
	public abstract class NamedElementInfo : IKeyedElementInfo<string>
	{
		/// <summary>
		/// �������������� ���������.
		/// </summary>
		protected NamedElementInfo(string name, string description, Type type)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException("name");
			if (type == null)
				throw new ArgumentNullException("type");

			Name = name;
			Description = description;
			Type = type;
		}

		/// <summary>
		/// ��� ����������.
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// �������� ����������.
		/// </summary>
		public string Description { get; private set; }

		/// <summary>
		/// ���, ����������� ����������.
		/// </summary>
		public Type Type { get; private set; }

		#region IKeyedElementInfo<string> Members
		/// <summary>
		/// ����.
		/// </summary>
		string IKeyedElementInfo<string>.Key
		{
			get { return Name; }
		}
		#endregion
	}
}