using System;

using JetBrains.Annotations;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// ��������� �������, ������������ ����������� �������.
	/// </summary>
	public interface IServicePublisher : IServiceProvider
	{
		/// <summary>
		/// ��������� ��������� ������� ���� �.
		/// </summary>
		[NotNull]
		IServiceRegistrationCookie Publish(
			[NotNull] Type serviceType,
			[NotNull] object serviceInstance);

		/// <summary>
		/// ��������� ������ ���� � � ���������� ��������������.
		/// </summary>
		[NotNull]
		IServiceRegistrationCookie Publish(
			[NotNull] Type serviceType,
			[NotNull] ServiceCreator serviceCreator);

		/// <summary>
		/// ������� ���������� �������.
		/// </summary>
		void Unpublish([NotNull] IServiceRegistrationCookie cookie);
	}
}