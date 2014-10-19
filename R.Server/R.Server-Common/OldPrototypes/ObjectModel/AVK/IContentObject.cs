using System;

using R.Server.Common.ObjectModel;

namespace R.Server.Common.ObjectModel
{
	public interface IContentObject : IRObject
	{
		IAccount Owner { get; set; }

		DateTime CreationDate { get; set; }

		DateTime LastModificationDate { get; set; }
	}
}