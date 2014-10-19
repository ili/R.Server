using System;

using JetBrains.Annotations;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// �������, ���������� ������.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	[MeansImplicitUse]
	public class ServiceAttribute : Attribute
	{
		/// <summary>
		/// �������������� ���������.
		/// </summary>
		public ServiceAttribute([NotNull] params Type[] contractTypes)
		{
			if (contractTypes == null)
				throw new ArgumentNullException("contractTypes");
			ContractTypes = contractTypes;
		}

		/// <summary>
		/// ��� �������.
		/// </summary>
		public Type[] ContractTypes { get; private set; }
	}
}