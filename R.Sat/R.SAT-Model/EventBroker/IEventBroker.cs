using System;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// ������ �������.
	/// </summary>
	public interface IEventBroker
	{
		/// <summary>
		/// ���������� ����������� � �������.
		/// </summary>
		void Fire<T>(string eventName, T arg);

		/// <summary>
		/// ����������� �� �������.
		/// </summary>
		IDisposable Subscribe<T>(string eventName, IObserver<T> observer);
	}
}