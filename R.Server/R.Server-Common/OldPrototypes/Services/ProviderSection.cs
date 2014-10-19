using System.Configuration;

namespace R.Server.Common.Services
{
	public class ProviderSection : ConfigurationSection
	{
		[ConfigurationProperty("providers")]
		public virtual ProviderSettingsCollection Providers
		{
			get { return (ProviderSettingsCollection)base["providers"]; }
		}

		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("defaultProvider", DefaultValue = "DefaultProvider")]
		public virtual string DefaultProvider
		{
			get { return (string)base["defaultProvider"]; }
			set { base["defaultProvider"] = value;        }
		}
	}
}