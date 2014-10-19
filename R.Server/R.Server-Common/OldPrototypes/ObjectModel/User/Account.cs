using System;
using System.Collections.Generic;
using BLToolkit.Common;
using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using BLToolkit.Validation;

using RoleType=R.Server.Common.ObjectModel.RoleType;

namespace R.Server.Common.ObjectModel
{
	[Serializable]
	public class Account : EntityBase<Account>
	{
		[MapField("AccountID")]
		[PrimaryKey, NonUpdatable]
		public int					AccountId;
		
		[Required]
		public RoleType		RoleType		= RoleType.User;

		[Required, MaxLength(100)]
		public string				NickName;
		
		[MaxLength(150)]
		public string				PrivateEmail;
		
		/// <summary>
		/// true - email is sent
		/// </summary>
		public bool					SendEmail;
		
		public EmailFormat	EmailFormat;

		/// <summary>
		/// User Title
		/// </summary>
		public string				Title;
		
		public int?					TitleColor;

		/// <summary>
		/// account creation date
		/// </summary>
		[Required, NonUpdatable]
		public DateTime				CreateTime;

		
		
		/// <summary>
		/// Set of dynamic properies, like "oridgin"
		/// </summary>
		public Dictionary<string, AccountProperty>	InfoProperies;

		/// <summary>
		/// Logins available for this account
		/// </summary>
		public ILogin[]				Logins;

		/// <summary>
		/// Statistic aggreagtes for account
		/// </summary>
		public AccountSummary		Summary;

		#region User Setting

		private Dictionary<string, IClientSetting>		_clientSettings		= new Dictionary<string, IClientSetting>();
		private Dictionary<string, ITemplateSetting>	_templateSettings	= new Dictionary<string, ITemplateSetting>();
		
		public void AddClientSetting(IClientSetting clientSetting)
		{
			_clientSettings.Add(clientSetting.ClientName, clientSetting);
		}
		
		public T GetClientSetting<T>(string ClientName)
			where T : class, IClientSetting
		{
			IClientSetting clientSetting;
			if (_clientSettings.TryGetValue(ClientName, out clientSetting))
				return (T)clientSetting;
			else 
				return null;
		}
		
		public void AddTemplateSetting(ITemplateSetting templateSetting)
		{
			_templateSettings.Add(templateSetting.TemplateName, templateSetting);
		}
		
		public T GetTemplateSetting<T>(string TemplateName)
			where T : class, ITemplateSetting
		{
			ITemplateSetting templateSetting;
			if (_templateSettings.TryGetValue(TemplateName, out templateSetting))
				return (T)templateSetting;
			else 
				return null;
		}
		
		#endregion



	}
}