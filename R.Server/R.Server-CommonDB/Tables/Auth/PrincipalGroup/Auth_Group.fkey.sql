ALTER TABLE Auth_PrincipalGroup
	ADD CONSTRAINT FK_Auth_PrincipalGroup_Auth_Group
	FOREIGN KEY (GroupID)
	REFERENCES Auth_Group (GroupID)