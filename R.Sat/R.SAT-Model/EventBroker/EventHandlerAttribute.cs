using System;

using JetBrains.Annotations;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// �������, ���������� ���������� �������.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	[MeansImplicitUse]
	public class EventHandlerAttribute : Attribute
	{
		private readonly string _eventName;

		/// <summary>
		/// �������������� ���������.
		/// </summary>
		public EventHandlerAttribute(string eventName)
		{
			_eventName = eventName;
		}

		/// <summary>
		/// ��� �������.
		/// </summary>
		public string EventName
		{
			get { return _eventName; }
		}
	}
}