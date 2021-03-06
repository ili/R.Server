﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace R.Server.Common
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Runtime.Serialization;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="R.Server")]
	public partial class AuthDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public AuthDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public AuthDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public AuthDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public AuthDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.Auth_GetPasswordHash", IsComposable=true)]
		public byte[] GetPasswordHash([global::System.Data.Linq.Mapping.ParameterAttribute(DbType="VarChar(64)")] string userName)
		{
			return ((byte[])(this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), userName).ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.Auth_GetLoginRoles", IsComposable=true)]
		public IQueryable<GetLoginRolesResult> GetLoginRoles([global::System.Data.Linq.Mapping.ParameterAttribute(DbType="VarChar(64)")] string authenType, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="VarChar(64)")] string identityName)
		{
			return this.CreateMethodCallQuery<GetLoginRolesResult>(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), authenType, identityName);
		}
	}
	
	[global::System.Runtime.Serialization.DataContractAttribute()]
	public partial class GetLoginRolesResult
	{
		
		private System.Guid _RoleID;
		
		private string _RoleName;
		
		public GetLoginRolesResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RoleID", DbType="UniqueIdentifier NOT NULL")]
		[global::System.Runtime.Serialization.DataMemberAttribute(Order=1)]
		public System.Guid RoleID
		{
			get
			{
				return this._RoleID;
			}
			set
			{
				if ((this._RoleID != value))
				{
					this._RoleID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RoleName", DbType="VarChar(64) NOT NULL", CanBeNull=false)]
		[global::System.Runtime.Serialization.DataMemberAttribute(Order=2)]
		public string RoleName
		{
			get
			{
				return this._RoleName;
			}
			set
			{
				if ((this._RoleName != value))
				{
					this._RoleName = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
