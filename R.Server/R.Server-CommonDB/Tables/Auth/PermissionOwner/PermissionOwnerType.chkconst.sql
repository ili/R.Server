/*
	1 - Principal
	2 - Role
*/
ALTER TABLE [dbo].[Auth_PermissionOwner]
	ADD CONSTRAINT [CK_Auth_PermissionOwner_PermissionOwnerType] 
	CHECK (PermissionOwnerType = 1 OR PermissionOwnerType = 2)
