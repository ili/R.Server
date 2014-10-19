using System;
using System.Collections.Generic;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// Базовая реализация менеджера расширений.
	/// </summary>
	public abstract class ExtensionManager<T, A> : IExtensionManager<T, A>, IServiceProvider
	{
		private readonly ServiceManager _serviceManager;

		/// <summary>
		/// Инициализирует экземпляр.
		/// </summary>
		protected ExtensionManager(IServiceProvider serviceProvider)
		{
			_serviceManager = serviceProvider == null
				? new ServiceManager()
				: new ServiceManager(serviceProvider);
			_serviceManager.Publish<IExtensionManager<T, A>>(this);
		}

		/// <summary>
		/// Внутренний ServiceManager.
		/// </summary>
		protected ServiceManager ServiceManager
		{
			get { return _serviceManager; }
		}

		#region IExtensionManager Members
		/// <summary>
		/// Сканирует сборку.
		/// </summary>
		public void Scan(T[] types, IExtensionAttachmentStrategy<T, A> strategy)
		{
			if (types == null)
				throw new ArgumentNullException("types");
			if (strategy == null)
				throw new ArgumentNullException("strategy");
			foreach (var t in types)
			{
				var context = new ExtensionAttachmentContext<T, A>(this, t);
				foreach (var attr in ExtractTypeAttributes(t, context))
					strategy.Attach(context, attr);
			}
		}
		#endregion

		#region IServiceProvider Members
		/// <summary>
		/// Возвращает сервис, реализующий интерфейс T
		/// </summary>
		public ST GetService<ST>() where ST : class
		{
			return _serviceManager.GetService<ST>();
		}
		#endregion

		/// <summary>
		/// Возвращает набор меток для переданного типа.
		/// </summary>
		protected abstract A[] GetAttributes(T type);

		private A[] ExtractTypeAttributes(
			T type,
			IExtensionAttachmentContext<T, A> context)
		{
			var attrs = GetAttributes(type);
			var filters = new List<IScanFilterAttribute<T, A>>();

			foreach (var attr in attrs)
			{
				if (attr is DisableAllScanAttribute)
					return EmptyArray<A>.Value;
				var sfa = attr as IScanFilterAttribute<T, A>;
				if (sfa != null)
					filters.Add(sfa);
			}

			var res = new List<A>();
			foreach (var attr in attrs)
			{
				var filtered = false;
				foreach (var sfa in filters)
					if (!sfa.AllowAttach(context, attr))
					{
						filtered = true;
						break;
					}
				if (!filtered)
					res.Add(attr);
			}
			return res.ToArray();
		}

		#region Implementation of IServiceProvider
		/// <summary>
		/// See <see cref="IServiceProvider.GetService"/>
		/// </summary>
		public object GetService(Type serviceType)
		{
			return _serviceManager.GetService(serviceType);
		}
		#endregion
	}
}