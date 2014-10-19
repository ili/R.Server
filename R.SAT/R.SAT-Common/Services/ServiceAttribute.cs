using System;

using JetBrains.Annotations;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// јтрибут, помечающий сервис.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	[MeansImplicitUse]
	public class ServiceAttribute : Attribute
	{
		/// <summary>
		/// »нициализиурет экземпл€р.
		/// </summary>
		public ServiceAttribute([NotNull] params Type[] contractTypes)
		{
			if (contractTypes == null)
				throw new ArgumentNullException("contractTypes");
			ContractTypes = contractTypes;
		}

		/// <summary>
		/// “ип сервиса.
		/// </summary>
		public Type[] ContractTypes { get; private set; }
	}
}