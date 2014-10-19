using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using BLToolkit.Validation;

using R.Server.Common.ObjectModel;

namespace R.Server.Common.ObjectModel
{
	public class WebLogin : ILogin
	{
		private int _loginId;
		private int _accountId;

		[PrimaryKey, NonUpdatable]
		[MapField("LoginID"), Required]
		public int				LoginId
		{
			get { return _loginId; }
			set { _loginId = value; }
		}

		[MapField("AccountID"), Required] 
		public int				AccountId
		{
			get { return _accountId; }
			set { _accountId = value; }
		}

		public LoginType		LoginType
		{
			get { return LoginType.WebLogin; }
		}

		[Required, MaxLength(150)] 
		public string			Password;
		
		[RequiredWithFormat("PasswordFormat", ErrorMessage = "Invalid password")]
		[MaxLength(150)] 
		public string			PasswordSalt;
		                           
		public PasswordFormat	PasswordFormat;
		
		[MaxLength(256)] 
		public string			PasswordQuestion;

		[RequiredWithFormat("AnswerFormat", ErrorMessage = "Invalid answer format")]
		[MaxLength(256)]
		public string PasswordAnswer;
	}
}