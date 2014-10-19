ALTER TABLE User_PrivateBaseProfile
	ADD CONSTRAINT FK_User_PrivateBaseProfile_Content_Content
	FOREIGN KEY (PrivateBaseProfileID, ContentType)
	REFERENCES Content_Content (ContentID, ContentType)