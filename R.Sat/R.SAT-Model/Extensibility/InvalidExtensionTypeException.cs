using System;
using System.Runtime.Serialization;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// ����������, ������������ ��� �������� ���� ����������.
	/// </summary>
	[Serializable]
	public class InvalidExtensionTypeException : ExtensibilityException
	{
		/// <summary>
		/// �������������� ���������.
		/// </summary>
		public InvalidExtensionTypeException() : base("Extensibility error")
		{
		}

		/// <summary>
		/// �������������� ��������� ��������� ���������.
		/// </summary>
		public InvalidExtensionTypeException(string message) : base(message)
		{
		}

		/// <summary>
		/// �������������� ��������� ��������� ��������� � ��������� �����������.
		/// </summary>
		public InvalidExtensionTypeException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>
		/// ����������� ��� ������������.
		/// ������ <see cref="Exception(SerializationInfo, StreamingContext)"/>
		/// </summary>
		protected InvalidExtensionTypeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}