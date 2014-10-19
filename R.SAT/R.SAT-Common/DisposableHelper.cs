using System;
using System.Collections.Generic;

using JetBrains.Annotations;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// Helper methods for <see cref="IDisposable"/>.
	/// </summary>
	public static class DisposableHelper
	{
		/// <summary>
		/// Вызывает Dispose всех элементов коллекции.
		/// </summary>
		public static void DisposeAll([NotNull] this IEnumerable<IDisposable> disposables)
		{
			if (disposables == null)
				throw new ArgumentNullException("disposables");

			foreach (var disp in disposables)
				disp.Dispose();
		}

		#region Disposable class
		private class Disposable : IDisposable
		{
			private readonly Action _disposeAction;

			public Disposable(Action disposeAction)
			{
				_disposeAction = disposeAction;
			}

			#region Implementation of IDisposable
			public void Dispose()
			{
				_disposeAction();
			}
			#endregion
		}
		#endregion

		/// <summary>
		/// Create disposable helper.
		/// </summary>
		public static IDisposable CreateDisposable(Action disposeAction)
		{
			return new Disposable(disposeAction);
		}
	}
}