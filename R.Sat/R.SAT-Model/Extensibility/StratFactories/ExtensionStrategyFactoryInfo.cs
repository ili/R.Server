using System;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// ���������� � ������������������ ������� ���������.
	/// </summary>
	public class ExtensionStrategyFactoryInfo : IKeyedElementInfo<Type>
	{
		private readonly Type _type;

		/// <summary>
		/// �������������� ���������.
		/// </summary>
		public ExtensionStrategyFactoryInfo(Type type)
		{
			_type = type;
		}

		/// <summary>
		/// ��� �������.
		/// </summary>
		public Type Type
		{
			get { return _type; }
		}

		#region IKeyedElementInfo<Type> Members
		/// <summary>
		/// ����.
		/// </summary>
		Type IKeyedElementInfo<Type>.Key
		{
			get { return Type; }
		}
		#endregion
	}
}