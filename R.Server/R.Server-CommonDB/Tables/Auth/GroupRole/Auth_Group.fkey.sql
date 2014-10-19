ALTER TABLE Auth_GroupRole
	ADD CONSTRAINT FK_Auth_GroupRole_Auth_Group
	FOREIGN KEY (GroupID)
	REFERENCES Auth_Group (GroupID)
