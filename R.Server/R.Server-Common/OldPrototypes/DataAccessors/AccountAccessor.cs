using System;
using System.Data;
using System.Collections.Generic;

using BLToolkit.Data;
using BLToolkit.DataAccess;

using R.Server.Common.ObjectModel;

using PasswordFormat=R.Server.Common.ObjectModel.PasswordFormat;
using RoleType=R.Server.Common.ObjectModel.RoleType;

namespace R.Server.Common.DataAccessors
{
	public abstract class AccountAccessor : PrimaryDataAccessor<AccountImpl>
	{
		public abstract int  GetCountByLoginName(string @loginName);
		public abstract int  GetCountByEmail    (string @email);
		public abstract void DeleteNotApproved  (DateTime @notActivatedSinceDate);
		public abstract void UnlockAccount      (int @id);
		public abstract void ChangePassword     (int @id, string @password, string @passwordSalt, PasswordFormat @passwordFormat);
		public abstract void ChangeRoleType     (DbManager db, int @id, RoleType @roleType);

		public abstract void ChangePasswordQuestionAndAnswer(
			int             @id,
			string          @passwordQuestion,
			string          @passwordAnswer,
			string          @answerSalt,
			PasswordFormat? answerFormat);

		public abstract List<AccountImpl> SelectPage       (                      int @pageIndex, int @pageSize, out int @totalRecords);
		public abstract List<AccountImpl> SelectPageByEmail(string @emailToMatch, int @pageIndex, int @pageSize, out int @totalRecords);
		public abstract List<AccountImpl> SelectPageByName (string @nameToMatch,  int @pageIndex, int @pageSize, out int @totalRecords);

		public abstract List<AccountImpl> SelectByRoleID       (int @roleID);
		public abstract List<AccountImpl> SelectByRoleIDAndName(int @roleID, string @nameToMatch);

		#region SelectOne

		[ActionName("SelectByID")]
		protected abstract IDataReader SelectByIDInternal(DbManager db, int @id);

		public AccountImpl SelectByID(int id)
		{
			return SelectAccount(delegate(DbManager db)
			{
				return SelectByIDInternal(db, id);
			});
		}

		[ActionName("SelectByName")]
		protected abstract IDataReader SelectByNameInternal(DbManager db, string @loginName);

		public AccountImpl SelectByName(string loginName)
		{
			return SelectAccount(delegate(DbManager db)
			{
				return SelectByNameInternal(db, loginName);
			});
		}

		[ActionName("SelectByGlobalID")]
		protected abstract IDataReader SelectByGlobalIDInternal(DbManager db, string @globalID);

		public AccountImpl SelectByGlobalID(string globalID)
		{
			return SelectAccount(delegate(DbManager db)
			{
				return SelectByGlobalIDInternal(db, globalID);
			});
		}

		[ActionName("SelectByEmail")]
		protected abstract IDataReader SelectByEmailInternal(DbManager db, string @email);

		public AccountImpl SelectByEmail(string email)
		{
			return SelectAccount(delegate(DbManager db)
			{
				return SelectByEmailInternal(db, email);
			});
		}

		private delegate IDataReader SelectReader(DbManager db);

		private AccountImpl SelectAccount(SelectReader selectReader)
		{
			using (DbManager   db = GetDbManager())
			using (IDataReader dr = selectReader(db))
			{
				if (dr.Read())
				{
					AccountImpl accountImpl = db.MappingSchema.MapDataReaderToObject<AccountImpl>(dr, null);

					if (dr.NextResult())
						while (dr.Read())
							accountImpl.Values.Add((string)dr["Code"], (string)dr["Value"]);

					return accountImpl;
				}
			}

			return null;
		}

		#endregion
	}
}