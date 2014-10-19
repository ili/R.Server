using System;
using System.Runtime.Serialization;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// ����������, ������������ � �������� ����������� ����������.
	/// </summary>
	[Serializable]
	public class ExtensibilityException : Exception
	{
		/// <summary>
		/// �������������� ���������.
		/// </summary>
		public ExtensibilityException() : base("Extensibility error")
		{
		}

		/// <summary>
		/// �������������� ��������� ��������� ���������.
		/// </summary>
		public ExtensibilityException(string message) : base(message)
		{
		}

		/// <summary>
		/// �������������� ��������� ��������� ��������� � ��������� �����������.
		/// </summary>
		public ExtensibilityException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>
		/// ����������� ��� ������������.
		/// ������ <see cref="Exception(SerializationInfo, StreamingContext)"/>
		/// </summary>
		protected ExtensibilityException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}