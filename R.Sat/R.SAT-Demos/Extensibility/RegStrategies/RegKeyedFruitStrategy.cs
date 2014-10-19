namespace Rsdn.SmartApp.Demos
{
	internal class RegKeyedFruitStrategy : RegKeyedElementsStrategy<string, FruitInfo, FruitAttribute>
	{
		public RegKeyedFruitStrategy(IServicePublisher publisher) : base(publisher)
		{}

		/// <summary>
		/// ������� �������.
		/// </summary>
		public override FruitInfo CreateElement(ExtensionAttachmentContext context, FruitAttribute attr)
		{
			return new FruitInfo(attr.Name, context.Type);
		}
	}
}
