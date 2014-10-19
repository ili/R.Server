using System;
using System.Collections.Generic;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// ������� ���������� <see cref="IRegElementsService{EI}"/>
	/// </summary>
	public class RegElementsService<TInfo> : IRegElementsService<TInfo> where TInfo : class
	{
		private readonly List<TInfo> _elements = new List<TInfo>();

		#region IRegElementsService<EI> Members
		/// <summary>
		/// ���������������� �������.
		/// </summary>
		public virtual void Register(TInfo elementInfo)
		{
			if (elementInfo == null)
				throw new ArgumentNullException("elementInfo");
			_elements.Add(elementInfo);
		}

		/// <summary>
		/// �������� ������ ������������������ ���������.
		/// </summary>
		public virtual TInfo[] GetRegisteredElements()
		{
			return _elements.ToArray();
		}
		#endregion
	}
}