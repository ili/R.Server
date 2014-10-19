using System;
using System.Collections.Generic;
using System.Collections.Specialized;

using BLToolkit.Common;
using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using BLToolkit.Validation;

namespace R.Server.Common.ObjectModel
{
	[Serializable]
	public class AccountImpl : EntityBase<AccountImpl>
	{
		#region Fields

		[MapField("AccountID"), PrimaryKey, NonUpdatable]
		public int             ID;
		[Required]                 public RoleType        RoleType = RoleType.User;

		// Registration info
		//
		[Required, MaxLength(50)]  public string          GlobalID;
		[Required, MaxLength(50)]  public string          LoginName;
		[Required, MaxLength(150)] public string          Password;
		[RequiredWithFormat("PasswordFormat", ErrorMessage = "Invalid password"),
		 MaxLength(150)] public string          PasswordSalt;
		public PasswordFormat  PasswordFormat;
		[          MaxLength(256)] public string          PasswordQuestion;
		[RequiredWithFormat("AnswerFormat", ErrorMessage = "Invalid answer format"),
		 MaxLength(256)] public string          PasswordAnswer;
		[RequiredWithFormat("AnswerFormat", ErrorMessage = "Invalid answer format"),
		 MaxLength(150)] public string          AnswerSalt;
		public PasswordFormat? AnswerFormat;
		[Required, MaxLength(100)] public string          NickName;
		[          MaxLength(150)] public string          Email;
		public bool            IsApproved;

		// Forum settings
		//
		public bool            SendEmail;          // true - email is sent
		public bool            FlatView;           // true - flat view, false - tree view
		[Required, MinValue(10)]   public short           TopicsPerPage = 10; // number of topics per page
		public EmailFormat     EmailFormat;

		// Rating settings
		//
		[Required, MinValue(1)]    public short           Rating        = 1;  // current user rating, calculated after each rating
		[Required, MinValue(1)]    public short           PersonalRates = 5;  // number of rates the user can give anyone personally
		[Required, MinValue(1)]    public short           TotalRates    = 20; // total rates the user can give anybody daily

		// Update statistic
		//
		[Required]                 public DateTime        LastUpdateDate;
		[Required, NonUpdatable]   public DateTime        CreateDate;    // account creation date

		// Ban info
		//
		public DateTime?       EndBanDate;

		// User Title
		//
		public string          Title;
		public int?            TitleColor;

		#endregion

		#region Public Members

		[MapIgnore]
		public bool IsLockedOut
		{
			get { return EndBanDate < DateTime.Now; }
		}

		[MapIgnore] public DateTime LastLoginDate;
		[MapIgnore] public DateTime LastActivityDate;

		public NameValueCollection Values = new NameValueCollection();

		#endregion

		#region Roles

		[MapIgnore] public bool IsAnonym     { get { return RoleType == RoleType.Anonym; } }
		[MapIgnore] public bool IsUser       { get { return RoleType == RoleType.User; } }
		[MapIgnore] public bool IsCustomRole { get { return RoleType == RoleType.Custom; } }

		private Dictionary<string,Role> RoleByName = new Dictionary<string,Role>();

		private List<Role> _roles = new List<Role>();
		public  List<Role>  Roles
		{
			get { return _roles; }
		}

		internal void AddRole(Role role)
		{
			Roles.     Add(role);
			RoleByName.Add(role.Name, role);
		}

		#endregion

		#region Validation

		[MapIgnore] public bool IsPasswordValid  { get { return IsValid("Password") && IsValid("PasswordSalt"); } }
		[MapIgnore] public bool IsLoginNameValid { get { return IsValid("LoginName"); } }
		[MapIgnore] public bool IsGlobalIDValid  { get { return IsValid("GlobalID");  } }

		public bool IsAnswerValid(bool required)
		{ 
			if (required && string.IsNullOrEmpty(PasswordAnswer))
				return false;

			return IsValid("PasswordAnswer");
		}

		public bool IsQuestionValid(bool required)
		{ 
			if (required && string.IsNullOrEmpty(PasswordQuestion))
				return false;

			return IsValid("PasswordQuestion");
		}

		public bool IsEmailValid(bool required)
		{ 
			if (required && string.IsNullOrEmpty(Email))
				return false;

			return IsValid("Email");
		}

		#endregion

		#region Validation Attributes

		class RequiredWithFormatAttribute : RequiredAttribute
		{
			public RequiredWithFormatAttribute(string formatField)
			{
				_formatField = formatField;
			}

			string _formatField;

			public override bool IsValid(ValidationContext context)
			{
				object pf = context.TypeAccessor[_formatField].GetValue(context.Object);

				return pf == null || (PasswordFormat)pf == PasswordFormat.Clear?
					!base.IsValid(context):
					base.IsValid(context);
			}
		}

		#endregion
	}
}