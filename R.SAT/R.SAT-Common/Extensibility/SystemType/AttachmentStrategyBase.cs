using System;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// Базовая реализация стратегии для <see cref="Type"/>.
	/// </summary>
	public abstract class AttachmentStrategyBase : AttachmentStrategyBase<Type, Attribute>
	{
		private readonly Type[] _supportedAttrTypes;

		/// <summary>
		/// Инициализирует экземпляр.
		/// </summary>
		protected AttachmentStrategyBase(params Type[] supportedAttrTypes)
		{
			_supportedAttrTypes = supportedAttrTypes;
		}

		/// <summary>
		/// Поддерживаемые типы.
		/// </summary>
		public Type[] SupportedAttrTypes
		{
			get { return _supportedAttrTypes; }
		}

		#region Overrides of AttachmentStrategyBase<Type,Attribute>
		/// <summary>
		/// Возвращает true, если атрибут соответствует стратегии.
		/// </summary>
		protected override bool IsSuitable(Attribute attribute)
		{
			foreach (var attrType in _supportedAttrTypes)
				if (attrType.IsAssignableFrom(attribute.GetType()))
					return true;
			return false;
		}
		#endregion
	}
}
