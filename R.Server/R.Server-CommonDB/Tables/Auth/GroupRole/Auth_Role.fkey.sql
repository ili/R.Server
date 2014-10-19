ALTER TABLE [dbo].[Auth_GroupRole]
	ADD CONSTRAINT [FK_Auth_GroupRole_Auth_Role] 
	FOREIGN KEY (RoleID)
	REFERENCES Auth_Role (RoleID)	

