using System;

namespace R.Server.Common.ObjectModel
{
	[Flags]
	public enum EmailFormat : byte
	{
		None     = 0,
		Text     = 1,
		Html     = 2,
		TextHtml = Text | Html
	}
}