ALTER TABLE [dbo].[Auth_Principal]
	ADD CONSTRAINT [FK_Auth_Principal_Auth_PermissionOwner] 
	FOREIGN KEY (PrincipalID)
	REFERENCES Auth_PermissionOwner (PermissionOwnerID)