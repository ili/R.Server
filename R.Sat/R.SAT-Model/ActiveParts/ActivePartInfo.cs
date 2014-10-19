using System;
using JetBrains.Annotations;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// �������� active part.
	/// </summary>
	public class ActivePartInfo
	{
		private readonly string _typeName;

		/// <summary>
		/// �������������� ���������.
		/// </summary>
		public ActivePartInfo([NotNull] string typeName)
		{
			_typeName = typeName;
			if (typeName == null)
				throw new ArgumentNullException("typeName");
		}

		/// <summary>
		/// ���, ����������� ����������.
		/// </summary>
		public string TypeName
		{
			get { return _typeName; }
		}
	}
}