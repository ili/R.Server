ALTER TABLE User_PrivateBaseProfile
	ADD CONSTRAINT FK_User_PrivateBaseProfile_User_User
	FOREIGN KEY (UserID)
	REFERENCES User_User (UserID)	

