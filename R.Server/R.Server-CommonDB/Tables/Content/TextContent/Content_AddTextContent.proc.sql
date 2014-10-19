CREATE PROCEDURE [dbo].[Content_AddTextContent]
	@id UNIQUEIDENTIFIER,
	@contentType INT,
	@creator UNIQUEIDENTIFIER,
	@body NTEXT
AS
	SET XACT_ABORT ON;
	BEGIN TRANSACTION;

	INSERT INTO Content_Content VALUES(@id, @contentType, @creator, GETDATE());
	INSERT INTO Content_TextContent VALUES(@id, @contentType, @body);

	COMMIT TRANSACTION;
RETURN 0;