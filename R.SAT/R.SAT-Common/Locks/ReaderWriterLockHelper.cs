using System;
using System.Threading;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// Реализация паттерна IDisposable для <see cref="ReaderWriterLock"/>
	/// </summary>
	public static class ReaderWriterLockHelper
	{
		/// <summary>
		/// Получить лок для читающего.
		/// </summary>
		public static IDisposable GetReaderLock(this ReaderWriterLock readerWriterLock)
		{
			readerWriterLock.AcquireReaderLock(Timeout.Infinite);
			return DisposableHelper.CreateDisposable(readerWriterLock.ReleaseReaderLock);
		}

		/// <summary>
		/// Получить лок для пишущего.
		/// </summary>
		public static IDisposable GetWriterLock(this ReaderWriterLock readerWriterLock)
		{
			readerWriterLock.AcquireWriterLock(Timeout.Infinite);
			return DisposableHelper.CreateDisposable(readerWriterLock.ReleaseWriterLock);
		}

		/// <summary>
		/// Получить лок для читающего с возможностью апгрейда до пишущего.
		/// </summary>
		public static IDisposable UpgradeToWriterLock(this ReaderWriterLock readerWriterLock)
		{
			var cookie = readerWriterLock.UpgradeToWriterLock(Timeout.Infinite);
			return DisposableHelper.CreateDisposable(() => readerWriterLock.DowngradeFromWriterLock(ref cookie));
		}
	}
}