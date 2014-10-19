ALTER TABLE Auth_PrincipalGroup
	ADD CONSTRAINT FK_Auth_PrincipalGroup_Auth_Principal
	FOREIGN KEY (PrincipalID)
	REFERENCES Auth_Principal (PrincipalID)	

