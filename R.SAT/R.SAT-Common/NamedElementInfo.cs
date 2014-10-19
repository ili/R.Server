using System;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// Базовая реализация описания именованного расширения.
	/// </summary>
	public abstract class NamedElementInfo : IKeyedElementInfo<string>
	{
		/// <summary>
		/// Инициализирует экземпляр.
		/// </summary>
		protected NamedElementInfo(string name, string description, Type type)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException("name");
			if (type == null)
				throw new ArgumentNullException("type");

			Name = name;
			Description = description;
			Type = type;
		}

		/// <summary>
		/// Имя расширения.
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// Описание расширения.
		/// </summary>
		public string Description { get; private set; }

		/// <summary>
		/// Тип, реализующий расширение.
		/// </summary>
		public Type Type { get; private set; }

		#region IKeyedElementInfo<string> Members
		/// <summary>
		/// Ключ.
		/// </summary>
		string IKeyedElementInfo<string>.Key
		{
			get { return Name; }
		}
		#endregion
	}
}