using System;
using JetBrains.Annotations;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// Описание active part.
	/// </summary>
	public class ActivePartInfo
	{
		private readonly string _typeName;

		/// <summary>
		/// Инициализирует экземпляр.
		/// </summary>
		public ActivePartInfo([NotNull] string typeName)
		{
			_typeName = typeName;
			if (typeName == null)
				throw new ArgumentNullException("typeName");
		}

		/// <summary>
		/// Тип, реализующий расширение.
		/// </summary>
		public string TypeName
		{
			get { return _typeName; }
		}
	}
}