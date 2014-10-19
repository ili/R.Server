using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Linq;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// Вспомогательный класс для создания экземпляров.
	/// </summary>
	public static class InstancingHelper
	{
		#region CreateInstance
		/// <summary>
		/// Создать экземпляр указанного типа.
		/// </summary>
		public static T CreateInstance<T>(
			IServiceProvider provider,
			params InstancingCustomParam[] customParams)
		{
			return (T) typeof (T).CreateInstance(provider, customParams);
		}

		/// <summary>
		/// Создать экземпляр указанного типа.
		/// </summary>
		public static object CreateInstance(
			this Type type,
			IServiceProvider provider,
			params InstancingCustomParam[] customParams)
		{
			var customParamsMap =
				customParams == null
					? new Dictionary<string, InstancingCustomParam>()
					: customParams.ToDictionary(prm => prm.Name);

			var ctor = FindCtor(type);
			var ctorParamValues = new List<object>();
			var ctorParams = ctor.GetParameters();
			var cpStartFrom = 0;
			if (ctorParams.Length > 0)
			{
				if (ctorParams[0].ParameterType == typeof (IServiceProvider))
				{
					ctorParamValues.Add(provider);
					cpStartFrom++;
				}
				for (var i = cpStartFrom; i < ctorParams.Length; i++)
				{
					InstancingCustomParam val;
					if (!customParamsMap.TryGetValue(ctorParams[i].Name, out val))
						throw new ArgumentException("Parameter '" + ctorParams[i].Name + "' not supplied");
					ctorParamValues.Add(val.Value);
				}
			}
			var custParamsHash = new HashSet<string>(ctorParams.Select(prm => prm.Name));
			foreach (var prm in customParamsMap.Values)
				if (!prm.Optional && !custParamsHash.Contains(prm.Name))
					throw new ArgumentException("Required parameter '{0}' not found in constructor".FormatStr(prm.Name));
			try
			{
				return ctor.Invoke(ctorParamValues.ToArray());
			}
			catch (TargetInvocationException ex)
			{
				throw ex.InnerException;
			}
		}

		private static ConstructorInfo FindCtor(Type type)
		{
			var ctors = type.GetConstructors();
			if (ctors.Length == 0)
				throw new ArgumentException("Type '" + type.FullName
					+ "' contains no public constructors.");
			if (ctors.Length == 1)
				return ctors[0];
			ConstructorInfo curCtor = null;
			foreach (var ctor in ctors)
			{
				if (!Attribute.IsDefined(ctor, typeof (DefaultConstructorAttribute)))
					continue;
				if (curCtor != null)
					throw new ArgumentException("Only one constructor can be marked by '"
						+ typeof (DefaultConstructorAttribute).FullName + "' attribute.");
				curCtor = ctor;
			}
			if (curCtor == null)
				throw new ArgumentException("No default constructor found.");
			return curCtor;
		}
		#endregion

		#region ISupportInitialize helper
		/// <summary>
		/// Allow to use using() construct for initialization scope.
		/// </summary>
		public static IDisposable InitScope(this ISupportInitialize component)
		{
			component.BeginInit();
			return DisposableHelper.CreateDisposable(component.EndInit);
		}
		#endregion
	}
}