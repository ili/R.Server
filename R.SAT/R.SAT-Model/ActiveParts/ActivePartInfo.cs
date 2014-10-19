using System;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// ќписание active part.
	/// </summary>
	public class ActivePartInfo
	{
		/// <summary>
		/// »нициализирует экземпл€р.
		/// </summary>
		public ActivePartInfo(Type type)
		{
			if (type == null)
				throw new ArgumentNullException("type");

			Type = type;
		}

		/// <summary>
		/// “ип, реализующий расширение.
		/// </summary>
		public Type Type { get; private set; }
	}
}