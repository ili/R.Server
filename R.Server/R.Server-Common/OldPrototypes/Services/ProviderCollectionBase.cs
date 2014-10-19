using System;
using System.Configuration.Provider;

namespace R.Server.Common.Services
{
	public class ProviderCollectionBase<P> : ProviderCollection
		where P : ProviderBase
	{
		public new P this[string name]
		{
			get { return (P) base[name]; }
		}

		public override void Add(ProviderBase provider)
		{
			if (provider == null) throw new ArgumentNullException("provider");

			if (!(provider is P))
				throw new ArgumentException("Invalid provider type", "provider");

			base.Add(provider);
		}
	}
}