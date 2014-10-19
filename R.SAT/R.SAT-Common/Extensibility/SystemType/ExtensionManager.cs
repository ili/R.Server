using System;
using System.Linq;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// Базовая реализация менеджера расширений для <see cref="Type"/>.
	/// </summary>
	public class ExtensionManager : ExtensionManager<Type, Attribute>, IExtensionManager
	{
		/// <summary>
		/// Инициализирует экземпляр.
		/// </summary>
		public ExtensionManager(IServiceProvider serviceProvider) : base(serviceProvider)
		{
			ServiceManager.Publish<IExtensionManager>(this);
		}

		#region Overrides of ExtensionManager<Type,Attribute>
		/// <summary>
		/// Возвращает набор меток для переданного типа.
		/// </summary>
		protected override Attribute[] GetAttributes(Type type)
		{
			return type.GetCustomAttributes(true).Cast<Attribute>().ToArray();
		}
		#endregion
	}
}
