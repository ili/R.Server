using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using BLToolkit.Validation;

using R.Server.Common.ObjectModel;

namespace R.Server.Common.ObjectModel
{
	public class WebSetting : IClientSetting
	{
		private string	_clientName;
		private int		_accountId;

		[MapField("AccountID"), Required]
		[PrimaryKey(1), NonUpdatable]
		public int		AccountId
		{
			get { return _accountId; }
			set { _accountId = value; }
		}

		[Required]
		[PrimaryKey(2), NonUpdatable]
		public string	ClientName
		{
			get { return _clientName; }
			set { _clientName = value; }
		}

		/// <summary>
		/// Name of used Template
		/// </summary>
		public string	TemplateName;

		public int		TopicPerPage;
		
	}
}