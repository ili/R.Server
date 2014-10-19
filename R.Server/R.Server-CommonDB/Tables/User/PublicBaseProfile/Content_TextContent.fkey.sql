ALTER TABLE User_PublicBaseProfile
	ADD CONSTRAINT FK_User_PublicBaseProfile_Content_TextContent
	FOREIGN KEY (PublicBaseProfileID, ContentType)
	REFERENCES Content_TextContent (TextContentID, ContentType)	

