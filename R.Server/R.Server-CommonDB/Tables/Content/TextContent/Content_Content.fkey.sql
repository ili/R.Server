ALTER TABLE Content_TextContent
	ADD CONSTRAINT FK_Content_TextContent_Content_Content
	FOREIGN KEY (TextContentID, ContentType)
	REFERENCES Content_Content (ContentID, ContentType)