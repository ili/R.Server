using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Security;

using R.Common.BusinessLogic;
using R.Common.ObjectModel;
using R.Common.Services;

using RPFormat = R.Common.ObjectModel.PasswordFormat;

namespace R.Web.Common.Providers
{
	public class RMembershipProvider : MembershipProvider
	{
		#region Init

		public override void Initialize(string name, NameValueCollection config)
		{
			base.Initialize(name, config);

			_applicationName = config["applicationName"];

			_requiresQuestionAndAnswer  = GetValue(config["requiresQuestionAndAnswer"],  false);
			_requiresUniqueEmail        = GetValue(config["requiresUniqueEmail"],        false);
			_enablePasswordRetrieval    = GetValue(config["enablePasswordRetrieval"],    false);
			_enablePasswordReset        = GetValue(config["enablePasswordReset"],        false);
			_minRequiredPasswordLength  = GetValue(config["minRequiredPasswordLength"],  0);
			_activationWindow           = GetValue(config["activationWindow"],           60 * 24);
			_passwordAttemptWindow      = GetValue(config["passwordAttemptWindow"],      0);
			_maxInvalidPasswordAttempts = GetValue(config["maxInvalidPasswordAttempts"], 5);
			_minRequiredNonAlphanumericCharacters = GetValue(config["minRequiredNonalphanumericCharacters"], 0);

			_passwordStrengthRegularExpression = config["passwordStrengthRegularExpression"];

			if (_passwordStrengthRegularExpression != null)
			{
				_passwordStrengthRegularExpression = _passwordStrengthRegularExpression.Trim();

				if (_passwordStrengthRegularExpression.Length != 0)
				{
					try
					{
						new Regex(_passwordStrengthRegularExpression);
					}
					catch (ArgumentException e)
					{
						throw new ProviderException(e.Message, e);
					}
				}
			}

			switch (config["passwordFormat"])
			{
				case "Clear":     _passwordFormat = MembershipPasswordFormat.Clear;     break;
				case "Encrypted": _passwordFormat = MembershipPasswordFormat.Encrypted; break;
				case "Hashed":    _passwordFormat = MembershipPasswordFormat.Hashed;    break;
			}
		}

		private bool GetValue(string value, bool defaultValue)
		{
			bool ret;
			return bool.TryParse(value, out ret)? ret: defaultValue;
		}

		private int GetValue(string value, int defaultValue)
		{
			int ret;
			return int.TryParse(value, out ret)? ret: defaultValue;
		}

		#endregion

		#region Public Properties

		private         string _applicationName;
		public override string  ApplicationName
		{
			get { return _applicationName;  }
			set { _applicationName = value; }
		}

		#endregion

		#region Protected Members

		private AccountManager _accountManager = ManagerService.CreateAccountManager();

		private int _activationWindow;

		protected DateTime NotActivatedSinceDate
		{
			get { return DateTime.UtcNow.AddMinutes(-_activationWindow); }
		}

		protected MembershipUser Convert(Account account)
		{
			return new MembershipUser(
				Name,
				account.LoginName,
				account.ID,
				account.Email,
				account.PasswordQuestion,
				account.Values["About"],
				account.IsApproved,
				account.IsLockedOut,
				account.CreateDate,
				account.LastLoginDate,
				account.LastActivityDate,
				account.LastUpdateDate,
				account.EndBanDate ?? DateTime.MinValue);
		}

		#endregion

		#region GetUser

		public override MembershipUser GetUser(string username, bool userIsOnline)
		{
			Account account = _accountManager.GetAccountByName(username);

			if (account == null)
				return null;

			if (userIsOnline)
				_accountManager.UpdateActivityDate(account);

			return Convert(account);
		}

		public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
		{
			Account account = _accountManager.GetAccountByID((int)providerUserKey);

			if (account == null)
				return null;

			if (userIsOnline)
				_accountManager.UpdateActivityDate(account);

			return Convert(account);
		}

		#endregion

		#region Password Support

		#region Protected Members

		protected virtual bool VerifyPassword(Account account, string password)
		{
			switch (account.PasswordFormat)
			{
				case RPFormat.Clear:     return account.Password == password;
				case RPFormat.Hashed:    return account.Password == CryptoService.ComputeHash(password, account.PasswordSalt);
				case RPFormat.Encrypted: return account.Password == CryptoService.Decrypt    (password, account.PasswordSalt);
			}

			return false;
		}

		protected virtual bool ValidatePassword(string username, string password)
		{
			if (password.Length < MinRequiredPasswordLength)
				return false;

			int count = 0;

			foreach (char c in password)
				if (!char.IsLetterOrDigit(c))
					count++;

			if (count < MinRequiredNonAlphanumericCharacters)
				return false;

			if (!string.IsNullOrEmpty(PasswordStrengthRegularExpression) &&
				!Regex.IsMatch(password, PasswordStrengthRegularExpression))
				return false;

			ValidatePasswordEventArgs e = new ValidatePasswordEventArgs(username, password, true);

			OnValidatingPassword(e);

			return !e.Cancel;
		}

		protected virtual void Encode(
			string       password,
			out string   encodedPassword,
			out string   salt,
			out RPFormat format)
		{
			switch (PasswordFormat)
			{
				case MembershipPasswordFormat.Clear:
					salt   = null;
					format = RPFormat.Clear;
					break;

				case MembershipPasswordFormat.Hashed:
					salt   = CryptoService.GenerateSalt();
					format = RPFormat.Hashed;
					break;

				case MembershipPasswordFormat.Encrypted:
					salt   = CryptoService.GenerateSalt();
					format = RPFormat.Encrypted;
					break;

				default:
					throw new InvalidOperationException();
			}

			encodedPassword = Encode(password, salt, format);
		}

		protected virtual void Encode(
			string        password,
			out string    encodedPassword,
			out string    salt,
			out RPFormat? format)
		{
			RPFormat f;

			Encode(password, out encodedPassword, out salt, out f);

			format = f;
		}

		protected virtual string Encode(string data, string salt, RPFormat format)
		{
			switch (format)
			{
				case RPFormat.Clear:     return data;
				case RPFormat.Hashed:    return CryptoService.ComputeHash(data, salt);
				case RPFormat.Encrypted: return CryptoService.Encrypt    (data, salt);
				default:
					throw new InvalidOperationException();
			}
		}

		protected virtual string Decode(string data, string salt, RPFormat format)
		{
			switch (format)
			{
				case RPFormat.Clear:     return data;
				case RPFormat.Encrypted: return CryptoService.Decrypt(data, salt);
				default:
					throw new InvalidOperationException();
			}
		}

		#endregion

		public override bool ChangePassword(string username, string oldPassword, string newPassword)
		{
			Account acc = _accountManager.GetAccountByName(username).Clone();

			if (acc == null ||
				!VerifyPassword(acc, oldPassword) ||
				!ValidatePassword(username, newPassword))
				return false;

			Encode(newPassword, out acc.Password, out acc.PasswordSalt, out acc.PasswordFormat);

			if (!acc.IsPasswordValid)
				return false;

			_accountManager.ChangePassword(acc);

			return true;
		}

		public override bool ChangePasswordQuestionAndAnswer(
			string username, string password, string newPasswordQuestion, string newPasswordAnswer)
		{
			if (newPasswordQuestion != null) newPasswordQuestion = newPasswordQuestion.Trim();
			if (newPasswordAnswer   != null) newPasswordAnswer   = newPasswordAnswer.  Trim();

			if (string.IsNullOrEmpty(newPasswordQuestion) ||
				string.IsNullOrEmpty(newPasswordAnswer))
				return false;

			Account acc = _accountManager.GetAccountByName(username).Clone();

			if (acc == null || !VerifyPassword(acc, password))
				return false;

			acc.PasswordQuestion = newPasswordQuestion;

			Encode(newPasswordAnswer, out acc.PasswordAnswer, out acc.AnswerSalt, out acc.AnswerFormat);

			if (!acc.IsAnswerValid(true) || !acc.IsQuestionValid(true))
				return false;

			_accountManager.ChangePasswordQuestionAndAnswer(acc);

			return true;
		}

		private          MembershipPasswordFormat _passwordFormat = MembershipPasswordFormat.Hashed;
		public  override MembershipPasswordFormat  PasswordFormat
		{
			get { return _passwordFormat; }
		}

		public override string GetPassword(string username, string answer)
		{
			if (!EnablePasswordRetrieval)
				throw new ProviderException("Password retrieval is not supported.");

			Account acc = _accountManager.GetAccountByName(username);

			if (acc.AnswerFormat == null || acc.PasswordFormat == RPFormat.Hashed)
				throw new ProviderException("Password retrieval is not supported for the account.");

			string encodedAnswer = Encode(answer.ToLower(), acc.AnswerSalt, (PasswordFormat)acc.AnswerFormat);

			if (encodedAnswer != acc.PasswordAnswer)
			{
				Thread.Sleep(2000);
				throw new MembershipPasswordException("Answer is invalid.");
			}

			return Decode(acc.Password, acc.PasswordSalt, acc.PasswordFormat);
		}

		public override string ResetPassword(string username, string answer)
		{
			if (!EnablePasswordReset)
				throw new ProviderException("Password reset is not supported.");

			Account acc = _accountManager.GetAccountByName(username).Clone();

			if (acc.AnswerFormat == null)
				throw new ProviderException("Password reset is not supported for the account.");

			string encodedAnswer = Encode(answer.ToLower(), acc.AnswerSalt, (PasswordFormat)acc.AnswerFormat);

			if (encodedAnswer != acc.PasswordAnswer)
			{
				Thread.Sleep(2000);
				throw new MembershipPasswordException("Answer is invalid.");
			}

			string newPassword = Membership.GeneratePassword(
				MinRequiredPasswordLength < 10 ? 10 : _minRequiredPasswordLength,
				MinRequiredNonAlphanumericCharacters);

			Encode(newPassword, out acc.Password, out acc.PasswordSalt, out acc.PasswordFormat);

			_accountManager.ChangePassword(acc);

			return newPassword;
		}

		#endregion

		#region User Support

		public override bool ValidateUser(string username, string password)
		{
			Account acc  = _accountManager.GetAccountByName(username);
			return  acc != null && VerifyPassword(acc, password);
		}

		public override MembershipUser CreateUser(
			string username,
			string password,
			string email,
			string passwordQuestion,
			string passwordAnswer,
			bool   isApproved,
			object providerUserKey,
			out MembershipCreateStatus status)
		{
			Account acc = new Account();

			acc.NickName         =
			acc.LoginName        = username.        Trim();
			acc.Email            = email.           Trim();
			acc.IsApproved       = isApproved;
			acc.PasswordQuestion = passwordQuestion == null? null: passwordQuestion.Trim();
			acc.GlobalID         = (providerUserKey ?? Guid.NewGuid()).ToString();

			// Encode & validate password.
			//
			if (!string.IsNullOrEmpty(password))
				Encode(password, out acc.Password, out acc.PasswordSalt, out acc.PasswordFormat);

			if (!ValidatePassword(username, password) || !acc.IsPasswordValid)
			{
				status = MembershipCreateStatus.InvalidPassword;
				return null;
			}

			// Encode & validate answer.
			//
			if (passwordAnswer != null)
				passwordAnswer = passwordAnswer.Trim();

			if (!string.IsNullOrEmpty(passwordAnswer))
				Encode(passwordAnswer.ToLower(), out acc.PasswordAnswer, out acc.AnswerSalt, out acc.AnswerFormat);

			if (!acc.IsAnswerValid(RequiresQuestionAndAnswer))
			{
				status = MembershipCreateStatus.InvalidAnswer;
				return null;
			}

			// Validate login name.
			//
			if (!acc.IsLoginNameValid)
			{
				status = MembershipCreateStatus.InvalidUserName;
				return null;
			}

			// Validate email.
			//
			if (!acc.IsEmailValid(RequiresUniqueEmail))
			{
				status = MembershipCreateStatus.InvalidEmail;
				return null;
			}

			// Validate question.
			//
			if (!acc.IsQuestionValid(RequiresQuestionAndAnswer))
			{
				status = MembershipCreateStatus.InvalidQuestion;
				return null;
			}

			// Validate GlobalID.
			//
			if (!acc.IsGlobalIDValid)
			{
				status = MembershipCreateStatus.InvalidProviderUserKey;
				return null;
			}

			_accountManager.DeleteNotApprovedAccounts(NotActivatedSinceDate);


			if (!_accountManager.CheckNewLoginName(username))
			{
				status = MembershipCreateStatus.DuplicateUserName;
				return null;
			}

			if (RequiresUniqueEmail && !_accountManager.CheckNewEmail(email))
			{
				status = MembershipCreateStatus.DuplicateEmail;
				return null;
			}

			_accountManager.CreateAccount(acc);

			status = MembershipCreateStatus.Success;
			return Convert(acc);
		}


		public override void UpdateUser(MembershipUser user)
		{
			Account acc = _accountManager.GetAccountByName(user.UserName).Clone();

			if (acc != null)
			{
				acc.Values["About"] = user.Comment;
				acc.IsApproved      = user.IsApproved;
				
				_accountManager.UpdateAccount(acc);
			}
		}

		public override bool DeleteUser(string username, bool deleteAllRelatedData)
		{
			Account acc = _accountManager.GetAccountByName(username);

			if (acc.IsApproved)
				return false;

			_accountManager.DeleteAccount(acc.ID);

			return true;
		}

		public override string GetUserNameByEmail(string email)
		{
			Account acc = _accountManager.GetAccountByEmail(email);
			return acc == null? null: acc.LoginName;
		}

		public override bool UnlockUser(string userName)
		{
			Account acc = _accountManager.GetAccountByName(userName);

			if (acc == null)
				return false;

			_accountManager.UnlockAccount(acc.ID);

			return true;
		}

		#endregion

		#region Get User List

		public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
		{
			return SelectUserCollection(out totalRecords, delegate(out int total)
			{
				return _accountManager.GetAccountList(pageIndex, pageSize, out total);
			});
		}

		public override MembershipUserCollection FindUsersByEmail(
			string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			return SelectUserCollection(out totalRecords, delegate(out int total)
			{
				return _accountManager.GetAccountListByEmail(emailToMatch, pageIndex, pageSize, out total);
			});
		}

		public override MembershipUserCollection FindUsersByName(
			string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			return SelectUserCollection(out totalRecords, delegate(out int total)
			{
				return _accountManager.GetAccountListByName(usernameToMatch, pageIndex, pageSize, out total);
			});
		}

		private delegate List<Account> SelectAccountList(out int totalRecords);

		private MembershipUserCollection SelectUserCollection(
			out int totalRecords, SelectAccountList selectList)
		{
			_accountManager.DeleteNotApprovedAccounts(NotActivatedSinceDate);

			MembershipUserCollection col = new MembershipUserCollection();

			foreach (Account a in selectList(out totalRecords))
				col.Add(Convert(a));

			return col;
		}

		#endregion

		#region Parameters

		private         bool _enablePasswordReset;
		public override bool  EnablePasswordReset
		{
			get { return _enablePasswordReset; }
		}

		private         bool _enablePasswordRetrieval;
		public override bool  EnablePasswordRetrieval
		{
			get { return _enablePasswordRetrieval; }
		}

		private         int _maxInvalidPasswordAttempts = 5;
		public override int  MaxInvalidPasswordAttempts
		{
			get { return _maxInvalidPasswordAttempts; }
		}

		private         int _minRequiredNonAlphanumericCharacters;
		public override int  MinRequiredNonAlphanumericCharacters
		{
			get { return _minRequiredNonAlphanumericCharacters; }
		}

		private         int _minRequiredPasswordLength;
		public override int  MinRequiredPasswordLength
		{
			get { return _minRequiredPasswordLength; }
		}

		private         int _passwordAttemptWindow;
		public override int  PasswordAttemptWindow
		{
			get { return _passwordAttemptWindow; }
		}

		private         string _passwordStrengthRegularExpression;
		public override string  PasswordStrengthRegularExpression
		{
			get { return _passwordStrengthRegularExpression; }
		}

		private         bool _requiresQuestionAndAnswer;
		public override bool  RequiresQuestionAndAnswer
		{
			get { return _requiresQuestionAndAnswer; }
		}

		private         bool _requiresUniqueEmail;
		public override bool  RequiresUniqueEmail
		{
			get { return _requiresUniqueEmail; }
		}

		#endregion

		#region Activity

		public override int GetNumberOfUsersOnline()
		{
			return _accountManager.GetActiveAccountCountSince(
				DateTime.UtcNow.AddMinutes(-Membership.UserIsOnlineTimeWindow));
		}

		#endregion
	}
}
