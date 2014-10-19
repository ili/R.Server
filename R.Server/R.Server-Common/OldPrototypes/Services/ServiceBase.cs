using System;
using System.Configuration;
using System.Configuration.Provider;
using System.Collections.Specialized;

using R.Server.Common.Services;

namespace R.Server.Common.Services
{
	public class ServiceBase<P>
		where P : ProviderBase, new()
	{
		private static P _provider = null;
		public  static P  Provider
		{
			get { return _provider; }
		}

		private static ProviderCollectionBase<P> _providers = new ProviderCollectionBase<P>();
		public  static ProviderCollectionBase<P>  Providers
		{
			get { return _providers; }
		}

		private static object _lock = new object();

		protected static void LoadProviders(string sectionName)
		{
			if (_provider == null)
			{
				lock (_lock)
				{
					if (_provider == null)
					{
						ProviderSection section = (ProviderSection)ConfigurationManager.GetSection(sectionName);

						foreach (ProviderSettings ps in section.Providers)
						{
							string s = (ps.Type ?? "").Trim();

							if (s == "")
								throw new ConfigurationErrorsException("Provider type name is empty.");

							Type type = Type.GetType(s, false, true);

							if (type == null)
								throw new ConfigurationErrorsException(string.Format(
									"Cant create type {0}", s));

							if (!typeof(P).IsAssignableFrom(type))
								throw new ConfigurationErrorsException(string.Format(
									"Provider {0} must implement the {1}type", s, typeof(P).FullName));

							P provider = (P)Activator.CreateInstance(type);

							provider.Initialize(ps.Name, new NameValueCollection(ps.Parameters));

							_providers.Add(provider);
						}

						_provider = _providers[section.DefaultProvider];

						if (_provider == null)
							_providers.Add(_provider = new P());
					}
				}
			}
		}
	}
}