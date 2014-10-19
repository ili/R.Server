namespace Rsdn.SmartApp.Demos
{
	internal class RegFruitStrategy : RegElementsStrategy<FruitInfo, FruitAttribute>
	{
		public RegFruitStrategy(IServicePublisher publisher) : base(publisher)
		{}

		/// <summary>
		/// ������� �������.
		/// </summary>
		public override FruitInfo CreateElement(
			ExtensionAttachmentContext context,
			FruitAttribute attr)
		{
			return new FruitInfo(attr.Name, context.Type);
		}
	}
}
