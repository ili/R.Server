CREATE PROCEDURE User_AddPublicBaseProfile_Base
	@id UNIQUEIDENTIFIER,
	@contentType INT,
	@creator UNIQUEIDENTIFIER,
	@body NTEXT,
	@realName VARCHAR(255),
	@origin VARCHAR(255)
AS
	SET XACT_ABORT ON
	BEGIN TRANSACTION;

	EXECUTE Content_AddTextContent @id, @contentType, @creator, @body;
	INSERT INTO User_PublicBaseProfile VALUES(@id, @contentType, @realName, @origin);

	COMMIT TRANSACTION;
RETURN 0;