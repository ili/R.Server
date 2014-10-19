CREATE PROCEDURE User_AddPublicBaseProfile
	@id UNIQUEIDENTIFIER,
	@creator UNIQUEIDENTIFIER,
	@body NTEXT,
	@realName VARCHAR(255),
	@origin VARCHAR(255)
AS
	DECLARE @contentType AS INT;
	SELECT @contentType = ShortID FROM Content_ContentType WHERE ContentTypeName = 'User.PublicBaseProfile';

	EXECUTE User_AddPublicBaseProfile_Base @id, @contentType, @creator, @body, @realName, @origin
RETURN 0;