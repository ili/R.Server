ALTER TABLE User_User
	ADD CONSTRAINT FK_User_User_Auth_Principal
	FOREIGN KEY (PrincipalID)
	REFERENCES Auth_Principal (PrincipalID)