ALTER TABLE [dbo].[Auth_Role]
	ADD CONSTRAINT [FK_Auth_Role_Auth_PermissionOwner] 
	FOREIGN KEY (RoleID, PermissionOwnerType)
	REFERENCES Auth_PermissionOwner (PermissionOwnerID, PermissionOwnerType)
