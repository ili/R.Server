using System;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// �������� active part.
	/// </summary>
	public class ActivePartInfo
	{
		/// <summary>
		/// �������������� ���������.
		/// </summary>
		public ActivePartInfo(Type type)
		{
			if (type == null)
				throw new ArgumentNullException("type");

			Type = type;
		}

		/// <summary>
		/// ���, ����������� ����������.
		/// </summary>
		public Type Type { get; private set; }
	}
}