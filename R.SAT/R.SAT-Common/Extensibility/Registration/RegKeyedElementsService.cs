using System;
using System.Collections.Generic;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// Базовая реализация <see cref="IRegKeyedElementsService{K, EI}"/>
	/// </summary>
	public class RegKeyedElementsService<K, EI> : RegElementsService<EI>,
		IRegKeyedElementsService<K, EI>
			where EI : class, IKeyedElementInfo<K>
			where K : class
	{
		private readonly Dictionary<K, EI> _byNameMap =
			new Dictionary<K, EI>();

		#region IRegKeyedElementsService<K,EI> Members
		/// <summary>
		/// Зарегистрировать элемент.
		/// </summary>
		public override void Register(EI elementInfo)
		{
			lock (_byNameMap)
			{
				if (_byNameMap.ContainsKey(elementInfo.Key))
					throw new ArgumentException("Element with name '" + elementInfo.Key
						+ "' already registered");
				base.Register(elementInfo);
				_byNameMap.Add(elementInfo.Key, elementInfo);
			}
		}

		/// <summary>
		/// Проверяет, есть ли зарегистрированный элемент с указанным именем.
		/// </summary>
		public bool ContainsElement(K key)
		{
			if (key == null)
				throw new ArgumentNullException("key");
			return _byNameMap.ContainsKey(key);
		}

		/// <summary>
		/// Возвращает элемент по его имени.
		/// </summary>
		public EI GetElement(K key)
		{
			if (key == null)
				throw new ArgumentNullException("key");
			EI elementInfo;
			if (_byNameMap.TryGetValue(key, out elementInfo))
				return elementInfo;
			throw new ArgumentException("Element with key '" + key
				+ "' is not registered");
		}
		#endregion
	}
}