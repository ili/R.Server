using System;
using System.Collections.Generic;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// Базовая реализация <see cref="IRegElementsService{EI}"/>
	/// </summary>
	public class RegElementsService<EI> : IRegElementsService<EI> where EI : class
	{
		private readonly List<EI> _elements = new List<EI>();

		#region IRegElementsService<EI> Members
		/// <summary>
		/// Зарегистрировать элемент.
		/// </summary>
		public virtual void Register(EI elementInfo)
		{
			if (elementInfo == null)
				throw new ArgumentNullException("elementInfo");
			_elements.Add(elementInfo);
		}

		/// <summary>
		/// Получить список зарегистрированных элементов.
		/// </summary>
		public virtual EI[] GetRegisteredElements()
		{
			return _elements.ToArray();
		}
		#endregion
	}
}