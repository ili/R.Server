ALTER TABLE Auth_PrincipalLogin
	ADD CONSTRAINT FK_Auth_PrincipalLogin_Auth_Principal
	FOREIGN KEY (PrincipalID)
	REFERENCES Auth_Principal (PrincipalID)