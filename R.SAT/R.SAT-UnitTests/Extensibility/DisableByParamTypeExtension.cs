using Rsdn.SmartApp.Extensibility.Registration;

namespace Rsdn.SmartApp.Extensibility
{
	[TestNamedElement(DisableByParamTypeExtension.Name)]
	[TestNamedElement(DisableByParamTypeExtension.Name2)]
	[DisableReg(DisableByParamTypeExtension.Name)]
	public class DisableByParamTypeExtension
	{
		public const string Name = "DisableByParamTypeExtension";
		public const string Name2 = "DisableByParamTypeExtension2";
	}
}