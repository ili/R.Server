ALTER TABLE Content_Content
	ADD CONSTRAINT FK_Content_Content_Content_ContentType
	FOREIGN KEY (ContentType)
	REFERENCES Content_ContentType (ShortID)